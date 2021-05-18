using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public EliasPlayer eliasPlayer;
    [SerializeField]
    private Canvas optionsMenu;
    [SerializeField]
    private Canvas controlsMenu;
    [SerializeField]
    private GameObject _title;
    private bool eliasEndMusic = false;

    void Start()
    {
        // Destroy old game objects that have DontDestroyOnLoad
        GameObject oldPlayer = GameObject.FindWithTag("Player");
        
        GameObject oldHud = FindObjectOfType<InventoryManager>()?.gameObject;
        if (oldPlayer != null)
        {
            Destroy(oldPlayer);

        }
        if (oldHud != null)
        {
            Destroy(oldHud.gameObject);

        }
    }
    
    public void PlayGame()
    {
        // music: trigger only if preset is not already running, to avoid unnessessary retriggering
        if (!eliasEndMusic)
        {
            eliasPlayer.RunActionPreset("Title End", true);
            eliasEndMusic = true;
        }

        SceneManager.LoadScene("StartingArea");
                
    }

    public void QuitGame()
    {
        Debug.Log("Quit");

        // music: trigger only if preset is not already running, to avoid unnessessary retriggering
        if (!eliasEndMusic)
        {
            eliasPlayer.RunActionPreset("Title End", true);
            eliasEndMusic = true;
        }

        Application.Quit();
    }

    public void ToggleOptions()
    {

        ToggleTitle();
        // make all main menu buttons clickable or disable and hide them
        //foreach (RectTransform button in transform)
        //{
        //    button.gameObject.SetActive(!button.gameObject.activeSelf);
        //}
        if (optionsMenu.gameObject.activeSelf)
        {
            SaveOptionManager.Instance.SaveVolumes();
        }
        optionsMenu.gameObject.SetActive(!optionsMenu.gameObject.activeSelf);
    }

    private void ToggleTitle()
    {
        _title.SetActive(!_title.activeSelf);
    }

    public void ToggleControls()
    {
        ToggleTitle();
        // make all main menu buttons clickable or disable and hide them
        foreach (RectTransform button in transform)
        {
            button.gameObject.SetActive(!button.gameObject.activeSelf);
        }
        controlsMenu.gameObject.SetActive(!controlsMenu.gameObject.activeSelf);


    }
}
