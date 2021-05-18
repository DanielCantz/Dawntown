using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// Author: Fred Newton, Akdogan : fa019
/// Description: Container for all SoundLines
/// ==============================================
/// Changelog: 
/// ==============================================
///

[CreateAssetMenu(menuName = "SoundContainer/SoundEvent")]
public class SoundEvent : ScriptableObject
{
    public string eventName;
    public List<SoundLine> soundLines;

    public SoundEvent()
    {
        eventName = "";
        soundLines = new List<SoundLine>();
    }
}
