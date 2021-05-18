///
/// Author: Christopher Beck
/// Description: This class defines the Portal in the starting area and Goal Room
/// ==============================================
/// Changelog:
/// 06/20/2020 - Christopher Beck - Added class
/// 07/09/2020 - Daniel Cantz - relocated DontDestroyOnLoad for player and Hud here, to ensure the player is not preserved, when he dies
/// 07/10/2020 - Daniel Cantz - player now takes items to the levels
/// 
/// ==============================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Portal : MonoBehaviour
{
    /// <summary>
    /// the name of the scene to load
    /// </summary>
    [SerializeField]
    private string sceneName;

    /// <summary>
    /// Should the stage be increased
    /// </summary>
    [SerializeField]
    private bool increaseStage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            StageIndicator stageIndicator = FindObjectOfType<StageIndicator>();
            if (increaseStage)
            {
                stageIndicator.IncreaseStage();
            }
            if (stageIndicator.getStage() >= 3)
            {
                // trigger end game screen
                InventoryManager.Instance.EndOfGameScreen.gameObject.SetActive(true);
                UIFader.Instance.UiElement = InventoryManager.Instance.EndOfGameScreen;
                UIFader.Instance.FadeIn();
            }
            else
            {
                //dont destroy player
                DontDestroyOnLoad(other.gameObject);
                //dont destroy inventory
                GameObject hud = GameObject.FindGameObjectWithTag("HUD");
                DontDestroyOnLoad(hud);
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
