using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

///
/// Author: Yannick Pfeifer yp009
/// Description: hud setter for the dialog system
/// ==============================================
/// Changelog:
/// Daniel Cantz: Added Fonts
/// ==============================================
///

public class DialogCanvas : Singleton<DialogCanvas>
{
    [SerializeField] private GameObject _dialogTextObject;
    [SerializeField] private GameObject _dialogImageObject;
    [SerializeField] private float _dialogScaling;

    [SerializeField] private Sprite _estellaDialogImage;
    [SerializeField] private Sprite _timotheDialogImage;



    public void setText(String text)
    {
        _dialogTextObject.GetComponent<TextMeshProUGUI>().SetText(text);
    }
    
    public void setImage(soundRole role)
    {
        switch (role)
        {
            case soundRole.Estella:
                _dialogImageObject.GetComponent<Image>().sprite = _estellaDialogImage;
                break;
            case soundRole.Tim:
                _dialogImageObject.GetComponent<Image>().sprite = _timotheDialogImage;
                break;
        }
    }
}
