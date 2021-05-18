using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySpriteSetter : MonoBehaviour
{
    [SerializeField]
    private ApplyElement _applyElement;
    [SerializeField]
    private ApplyElementChance _applyElementChance;

    private void Start()
    {
        ElementEnum currentElement = _applyElement ? _applyElement.CurrentElement.ElementEnum : _applyElementChance.CurrentElement.ElementEnum;        
        GetComponent<Animator>().SetInteger("element", (int)currentElement);
    }
}
