using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
///
/// Author: Samuel Müller: sm184
/// Description: Holds information about the current language selected. OnChange written texts will updated accordingly
/// ==============================================
/// Changelog: 
/// ==============================================
///
public class LanguageManager : Singleton<LanguageManager>
{
    [SerializeField] private LanguageEnum _language;

    public LanguageEnum Language { get => _language; }

    public delegate void OnLanguageChange();

    public OnLanguageChange onLanguageChange;

    public void SetLanguage(int value)
    {
        if (value <= (Enum.GetNames(typeof(LanguageEnum)).Length))
        {
            _language = (LanguageEnum)value;
            onLanguageChange?.Invoke();
        }
    }

    public void SetLanguageAndSave(int value)
    {
        SetLanguage(value);
        SaveOptionManager.Instance.SaveLanguage();
    }
}
public enum LanguageEnum
{
    English = 0,
    German = 1
}
