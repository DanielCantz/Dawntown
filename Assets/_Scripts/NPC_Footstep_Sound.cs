using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Footstep_Sound : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string fmodFootstepEvent_A, fmodFootstepEvent_B;
    private FMOD.Studio.EventInstance fmodFootstepInstance_A, fmodFootstepInstance_B;

    Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        fmodFootstepInstance_A = FMODUnity.RuntimeManager.CreateInstance(fmodFootstepEvent_A);
        fmodFootstepInstance_B = FMODUnity.RuntimeManager.CreateInstance(fmodFootstepEvent_A);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(fmodFootstepInstance_A, GetComponent<Transform>(), _rigidbody);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(fmodFootstepInstance_A, GetComponent<Transform>(), _rigidbody);
    }

    public void triggerFootstepASound()
    {
        fmodFootstepInstance_A.start();
        //print("Audio: ");

    }

    public void triggerFootstepBSound()
    {
        fmodFootstepInstance_B.start();
        //print("Audio: ");

    }
}