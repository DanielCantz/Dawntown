using System;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Events;

///
/// Author: Samuel Müller: sm184
/// Description: Manages the current active hud elements, that cause the game to pause the game (e.g. inventory, Pause Menu).
/// ==============================================
/// Changelog: 
/// ==============================================
///
public class PopUpManager : Singleton<PopUpManager>
{
    [SerializeField] private GameObject _inventory;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _dialogCanvas;
    [SerializeField] private ItemTooltip _itemTooltip;

    public UnityEvent OnInventoryClose;

    public enum ActiveMenuEnum
    {
        None,
        Inventory,
        Pause
    }
    private ActiveMenuEnum _activeMenu = ActiveMenuEnum.None;

    public bool IsGamePaused()
    {
        return _activeMenu != ActiveMenuEnum.None;
    }

    private void Start()
    {
        _inventory.SetActive(false);
        _pauseMenu.SetActive(false);
        _dialogCanvas.SetActive(false);
    }

    private void Update()
    {
        #region closeMenues
        if (_activeMenu == ActiveMenuEnum.Inventory)
        {
            if (Input.GetButtonDown("Inventory") || Input.GetButtonDown("Cancel"))
            {
                DisableMenu(_inventory);
                _itemTooltip.HideTooltip();
                OnInventoryClose?.Invoke();
            }
            return;
        }
        if (_activeMenu == ActiveMenuEnum.Pause)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                _pauseMenu.GetComponent<PauseMenu>().Back();
            }
            return;
        }
        #endregion
        #region openMenues
        if (_activeMenu == ActiveMenuEnum.None)
        {
            if (Input.GetButtonDown("Inventory"))
            {
                _activeMenu = ActiveMenuEnum.Inventory;
                _inventory.SetActive(true);
                ToggleTime();
                return;
            }
            if (Input.GetButtonDown("Cancel"))
            {
                _activeMenu = ActiveMenuEnum.Pause;
                _pauseMenu.SetActive(true);
                ToggleTime();
            }
        }
        #endregion
    }

    private void ToggleTime()
    {
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
    }

    private void DisableMenu(GameObject menu)
    {
        menu.SetActive(false);
        ToggleTime();
        _activeMenu = ActiveMenuEnum.None;
    }

    public void DisablePause()
    {
        DisableMenu(_pauseMenu);
    }

    public void DisableInventory()
    {
        DisableMenu(_inventory);
    }

    public void EnableDialog()
    {
        _dialogCanvas.SetActive(true);
    }
    
    public void DisableDialog()
    {
        _dialogCanvas.SetActive(false);
    }

    public void setDialog(String text, soundRole role)
    {
        _dialogCanvas.GetComponent<DialogCanvas>().setText(text);
        _dialogCanvas.GetComponent<DialogCanvas>().setImage(role);
    }
}

