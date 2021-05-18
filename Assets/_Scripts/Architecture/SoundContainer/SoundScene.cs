using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// Author: Fred Newton, Akdogan : fa019
/// Description: SoundScene is an Container for all SoundEvents
/// ==============================================
/// Changelog: 
/// ==============================================
///

[CreateAssetMenu(menuName = "SoundContainer/SoundScene")]
public class SoundScene : ScriptableObject
{
    public string sceneName;
    public SoundEvent[] soundEvents;

    public SoundScene()
    {
        sceneName = "test";
    }
}
