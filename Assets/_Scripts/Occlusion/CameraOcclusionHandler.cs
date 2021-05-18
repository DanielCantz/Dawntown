using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOcclusionHandler : MonoBehaviour
{
    public Transform _obstruction = null;
    public Transform _oldObstruction = null;
    public float maxDistance = 4.5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        ViewObstructed();
    }

    void ViewObstructed()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            if (hit.collider.gameObject.tag != "Player")
            {
                _obstruction = hit.transform.parent;

                Transform wrapper = _obstruction.transform.Find("Houses");
                if (wrapper != null)
                {
                    var meshList = wrapper.gameObject.GetComponentsInChildren<MeshRenderer>();
                    if (_obstruction != _oldObstruction && _oldObstruction != null)
                    {
                        Transform oldwrapper = _oldObstruction.transform.Find("Houses");
                        if(oldwrapper != null)
                        {
                            var oldMeshList = oldwrapper.gameObject.GetComponentsInChildren<MeshRenderer>();

                            foreach (var mesh in oldMeshList)
                            {
                                mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                            }
                        }
                    }

                    foreach (var mesh in meshList)
                    {
                        mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                    }


                    _oldObstruction = _obstruction;
                }
            }
        }
        else if (_obstruction != null)
        {
            Transform oldWrapper = _obstruction.transform.Find("Houses");
            if (oldWrapper != null)
            {
                var meshList = oldWrapper.gameObject.GetComponentsInChildren<MeshRenderer>();
                foreach (var mesh in meshList)
                {
                    mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                }
            }

            _obstruction = null;
        }

    }
}
