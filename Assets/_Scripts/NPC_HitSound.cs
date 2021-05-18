using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_HitSound : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string fmodHitEvent;
    private FMOD.Studio.EventInstance fmodPcHitInstance;

    Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        fmodPcHitInstance = FMODUnity.RuntimeManager.CreateInstance(fmodHitEvent);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(fmodPcHitInstance, GetComponent<Transform>(), _rigidbody);
    }

    public void triggerHitSound()
    {
        fmodPcHitInstance.start();
        print("Audio: triggerHitSound");

    }
}
