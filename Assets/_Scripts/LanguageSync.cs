using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LanguageSync : MonoBehaviour
{
    [SerializeField][TextArea]
    private string de;
    [SerializeField][TextArea]
    private string en;

    private TextMeshProUGUI _text;
    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        LanguageManager.Instance.onLanguageChange += SyncLanguage;
        SyncLanguage();
    }

    private void SyncLanguage()
    {
        switch (LanguageManager.Instance.Language)
        {
            case LanguageEnum.German:
                _text.text = de;
                break;
            default:
                _text.text = en;
                break;
        }
    }
}
