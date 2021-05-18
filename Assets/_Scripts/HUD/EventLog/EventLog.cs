using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventLog : Singleton<EventLog>
{
    [SerializeField]
    private GameObject _eventLog;

    [SerializeField]
    private TextMeshProUGUI _textMeshPro;

    private List<string> ListOfEvents = new List<string>();

    private readonly int MAX_EVENTS = 3;

    //Called when Item is dropped, creates a string that fits the case in the correct game language and uses it as parameter for the AddEvent function call
    public void ItemDropped(Item item)
    {
        AbilityHUDElement itemInfo = item.HudInfo;
        string eventString;
        switch (GetGameLanguage())
        {
            case LanguageEnum.English: eventString = "You dropped \"" + itemInfo.Description.Title + "\"-Tier " + item.Tier + " from your inventory."; break;
            case LanguageEnum.German: eventString = "Du hast \"" + itemInfo.Description.Title + "\"-Tier " + item.Tier + " aus deinem Inventar fallen gelassen."; break;
            default: eventString = "You dropped \"" + itemInfo.Description.Title + "\"-Tier " + item.Tier + " from your inventory."; break;
        }
        AddEvent(eventString);
    }

    //Called when Item is deleted, creates a string that fits the case in the correct game language and uses it as parameter for the AddEvent function call
    public void ItemDeleted(Item item)
    {
        AbilityHUDElement itemInfo = item.HudInfo;
        string eventString;
        switch (GetGameLanguage())
        {
            case LanguageEnum.English: eventString = "You deleted \"" + itemInfo.Description.Title + "\"-Tier " + item.Tier + " from your inventory."; break;
            case LanguageEnum.German: eventString = "Du hast \"" + itemInfo.Description.Title + "\"-Tier " + item.Tier + " aus deinem Inventar gelöscht."; break;
            default: eventString = "You deleted \"" + itemInfo.Description.Title + "\"-Tier " + item.Tier + " from your inventory."; break;
        }
        AddEvent(eventString);
    }

    //Called when Item is collected, creates a string that fits the case in the correct game language and uses it as parameter for the AddEvent function call
    public void ItemCollected(Item item)
    {
        AbilityHUDElement itemInfo = item.HudInfo;
        string eventString;
        switch (GetGameLanguage())
        {
            case LanguageEnum.English: eventString = "You collected \"" + itemInfo.Description.Title + "\"-Tier " + item.Tier + " and put it in your inventory."; break;
            case LanguageEnum.German: eventString = "Du hast \"" + itemInfo.Description.Title + "\"-Tier " + item.Tier + " aufgehoben und es in dein Inventar gelegt."; break;
            default: eventString = "You collected \"" + itemInfo.Description.Title + "\"-Tier " + item.Tier + " and put it in your inventory."; break;
        }
        AddEvent(eventString);
    }

    //Adds an Event to the EventList and prints it on screen for 4 seconds
    private void AddEvent(string eventString)
    {
        string _guiText;
        ListOfEvents.Add(eventString);

        if(ListOfEvents.Count >= MAX_EVENTS)
        {
            ListOfEvents.RemoveAt(0);
        }
        _guiText = "";

        foreach (string logEvent in ListOfEvents)
        {
            _guiText += logEvent;
            _guiText += "\n";
        }
        _textMeshPro.SetText(_guiText);
        ShowEventLog();
        Invoke("HideEventLog", 4);
    }

    //EventLog visible
    private void ShowEventLog()
    {
        gameObject.SetActive(true);
    }

    //EventLog hidden
    private void HideEventLog()
    {
        ListOfEvents.Clear();
        gameObject.SetActive(false);
    }

    //Get current Game Language
    private LanguageEnum GetGameLanguage()
    {
        return LanguageManager.Instance.Language;
    }
}
