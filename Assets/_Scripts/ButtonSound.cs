using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour 
{
    public string FmodEvent;
    Button btn;

    void Start()
    {
        btn = this.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }


    void Update()
    {

    }

    void TaskOnClick()
    {
        FMODUnity.RuntimeManager.PlayOneShot(FmodEvent);
    }

}
