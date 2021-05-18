///
/// Author: Christopher Beck
/// Description: This class defines the Loading Screen functionality
/// ==============================================
/// Changelog:
/// 06/20/2020 - Christopher Beck - Added class
/// ==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadMap : MonoBehaviour
{

    [SerializeField]
    private string sceneName;

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        // The Application loads the Scene in the background as the current Scene runs.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
