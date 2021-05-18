///
/// Author: Daniel Cantz
/// Description: Manages the different events that occur in the starting Area
/// ==============================================
/// Changelog: 
/// ==============================================
///

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class showOnApproach : MonoBehaviour
{
    [SerializeField] Canvas _canvas;
    [SerializeField] TMP_Text _textField;
    [SerializeField] string _textGer;
    [SerializeField] string _textEng;
    [SerializeField] int approachRadius = 3;
    LanguageEnum currentLang;
    LanguageEnum lang;
    GameObject player;
    bool wasActive = false;
    bool isActive = false;



    // Start is called before the first frame update
    void Start()
    {
        currentLang = LanguageManager.Instance.Language;
        player = GameObject.FindGameObjectWithTag("Player");

        if (currentLang == LanguageEnum.German)
        {
            _textField.text = _textGer;
        }
        else if(currentLang == LanguageEnum.English)
        {
            _textField.text = _textEng;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        if (Vector3.Distance(playerPos, transform.position) <= approachRadius)
        {
            isActive = true;
        }
        else
        {
            isActive = false;
        }
        
        if(isActive && !wasActive)
        {
            _canvas.gameObject.SetActive(true);
        }
        else if(!isActive && wasActive)
        {
            _canvas.gameObject.SetActive(false);

        }

        wasActive = isActive;


        // TODO falls sprache umgestellt wird, text ändern
        lang = LanguageManager.Instance.Language;
        if (lang != currentLang)
        {
            if (lang == LanguageEnum.German)
            {
                _textField.text = _textGer;
            }
            else if (lang == LanguageEnum.English)
            {
                _textField.text = _textEng;
            }
            currentLang = lang;
        }

    }


}
