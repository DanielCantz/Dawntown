///
/// Author: dc029
/// Description: when attached to an game object the object stays instantiated after switching scenes
/// ==============================================
/// Changelog: 
/// ==============================================
///

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
