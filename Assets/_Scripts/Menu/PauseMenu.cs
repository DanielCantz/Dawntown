///
/// Author: Christopher Beck
/// Description: This class defines functions for the pause menu, including changing the quality settings
/// ==============================================
/// Changelog:
/// 06/29/2020 - Christopher Beck - Created class
/// 07/10/2020 - removed toggle function -> 
/// 07/12/2020 - refactored and added pause menu states.
/// ==============================================
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : Singleton<PauseMenu>
{
    [SerializeField] private GameObject _mainPause;
    [SerializeField] private GameObject _graphicsSettings;
    [SerializeField] private GameObject _volumeSettings;

    private enum PauseMenuStateEnum
    {
        Idle,
        Graphics,
        Volume
    }
    private PauseMenuStateEnum _menuState;

    private void OnEnable()
    {
        MainPause();
    }

    public void MainPause()
    {
        SwitchMenuState(PauseMenuStateEnum.Idle);
    }

    public void OpenVolumeSettings()
    {
        SwitchMenuState(PauseMenuStateEnum.Volume);
    }

    public void OpenGraphicSettings()
    {
        SwitchMenuState(PauseMenuStateEnum.Graphics);
    }

    public void Back()
    {
        if (_menuState != PauseMenuStateEnum.Idle)
        {
            if (_menuState == PauseMenuStateEnum.Volume)
            {
                SaveOptionManager.Instance.SaveVolumes();
            }
            MainPause();
        }
        else
        {
            PopUpManager.Instance.DisablePause();
        }
    }

    private void SwitchMenuState(PauseMenuStateEnum futureState)
    {
        _mainPause.SetActive(false);
        _graphicsSettings.SetActive(false);
        _volumeSettings.SetActive(false);
        _menuState = futureState;
        switch (_menuState)
        {
            case PauseMenuStateEnum.Idle:
                _mainPause.SetActive(true);
                break;
            case PauseMenuStateEnum.Graphics:
                _graphicsSettings.SetActive(true);
                break;
            case PauseMenuStateEnum.Volume:
                _volumeSettings.SetActive(true);
                break;
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}