using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// Author: Fred Newton, Akdogan : fa019
/// Description: Singleton that holds all SoundScenes.
/// ==============================================
/// Changelog: 
/// ==============================================
///

public class SoundContainer : MonoBehaviour
{
    public static SoundContainer Instance { get; private set; }
    public SoundScene[] soundScenes;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
