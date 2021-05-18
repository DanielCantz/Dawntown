using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Element")]
///
/// Author: Samuel Müller, sm184
/// Description: Holds all for element relevant values
/// ==============================================
/// Changelog:
/// ==============================================
///
public class Element : AbstractAbilityComponent
{
    // Offensive Variables
    [SerializeField] private float _baseDuration = 1f;
    //Defensive Variables
    [SerializeField] private ElementEnum _element;
    [SerializeField] private List<ElementTierStatHolder> _tierStats = new List<ElementTierStatHolder>(3);

    public float Value { get => _tierStats[Tier - 1].Value; }

    public float Chance { get => _tierStats[Tier - 1].Chance; }

    public float Duration { get => _baseDuration; set => _baseDuration = value; }
    public float Delay { get => _tierStats[Tier - 1].Delay; }
    public float Strength { get => _tierStats[Tier - 1].Strength; }

    internal ElementEnum ElementEnum { get => _element; set => _element = value; }
}
