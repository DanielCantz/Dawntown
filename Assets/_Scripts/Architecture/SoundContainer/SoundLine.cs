using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum soundRole { Estella, Tim }

///
/// Author: Fred Newton, Akdogan : fa019
/// Description: Soundline for each sound Event
/// ==============================================
/// Changelog: 
/// ==============================================
///

[CreateAssetMenu(menuName = "SoundContainer/SoundLine")]
public class SoundLine : ScriptableObject
{
    public string lineID;
    public string nextLineID;
    public soundRole role;
    public float waitTimeInMS;
    public string subtileGer;
    public string subtileEng;
    public AudioClip lineAudio;

    public SoundLine()
    {
        lineID = "0000";
        nextLineID = "End";
        role = soundRole.Estella;
        waitTimeInMS = 0;
        subtileGer = "";
        subtileEng = "";
    }
}
