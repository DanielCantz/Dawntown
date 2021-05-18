using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Footstep_Sound : MonoBehaviour
{
    [FMODUnity.EventRef]
    private FMOD.Studio.EventInstance fmodInstance;
    Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        fmodInstance = FMODUnity.RuntimeManager.CreateInstance("event:/PC/Footsteps");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(fmodInstance, GetComponent<Transform>(), _rigidbody);        
    }

    public void triggerFootstep()
    {
        fmodInstance.start();
    }
}
