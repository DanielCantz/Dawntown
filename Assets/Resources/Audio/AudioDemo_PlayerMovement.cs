using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDemo_PlayerMovement : MonoBehaviour
{
    Rigidbody _rigidbody;
    Vector3 position, move, jump;
    public float speed = 4.0f;
    public float jumpStrength = 8.0f;
    public bool isWalking = false;

    [FMODUnity.EventRef]
    public string fmodJumpEvent, fmodWalkLoop;
    private FMOD.Studio.EventInstance fmodWalkInstance;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        jump = new Vector3(0, jumpStrength, 0);
        fmodWalkInstance = FMODUnity.RuntimeManager.CreateInstance(fmodWalkLoop);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(fmodWalkInstance, GetComponent<Transform>(), _rigidbody);
        fmodWalkInstance.start();
    }

    
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        
        fmodWalkInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        if (Mathf.Abs(move.x) >= 0.01f || Mathf.Abs(move.z) >= 0.01f)
        {
            if (!isWalking)
            {
                fmodWalkInstance.setParameterByName("Movement", 1, false);
                isWalking = true;
            } 
        }
        else if (isWalking)
        {
            fmodWalkInstance.setParameterByName("Movement", 0, false);
            isWalking = false;
        }


        position = _rigidbody.position;
        move = new Vector3(horizontal, 0, vertical);

        position = position + move * speed * Time.deltaTime;
        _rigidbody.MovePosition(position);

        if (Input.GetButtonDown("Jump"))
        {
            _rigidbody.AddForce(jump, ForceMode.VelocityChange);
            FMODUnity.RuntimeManager.PlayOneShot(fmodJumpEvent);
        }

    }

    bool IsPlaying(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }



}
