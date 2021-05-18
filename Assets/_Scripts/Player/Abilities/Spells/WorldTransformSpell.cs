using UnityEngine;
using System.Collections;
using System.Collections.Generic;
///
/// Author: Daniel Cantz dc029, Yannick Pfeifer yp009
/// Description: Defensive spells abstract
/// ==============================================
/// Changelog:
/// 25.06.2020 - Samuel Müller - added TierBasedStats for Tier implementation.
/// ==============================================
///
[CreateAssetMenu(menuName = "Abilities/WorldTransformSpell")]
public class WorldTransformSpell : AbstractSpell
{
    [SerializeField] private GameObject _worldObject;
    [SerializeField] private float _baseLifetime = 1f;
    [SerializeField] private float _range;
    [SerializeField] private float _basePlacementOffset;
    [SerializeField] private List<WorldTransformTierStatHolder> _tierStats = new List<WorldTransformTierStatHolder>(Item.TIER_RANGE);
    
    private float _placementOffset;
    
    private WorldTransformTriggerable _launcher;
    private float _additionalLifetime;

    //private Transform bulletSpawn;

    public GameObject WorldObject { get => _worldObject; set => _worldObject = value; }
    public float Lifetime {
        get {
            return _tierStats[Tier-1].Lifetime + _additionalLifetime;
        }
    }

    public float Scale
    {
        get
        {
            return _tierStats[Tier - 1].Scale;
        }
    }

    public void IncreaseLifetime(float additionalLifetime)
    {
        _additionalLifetime = additionalLifetime;
    }

    public float Range { get => _range; set => _range = value; }
    public WorldTransformTriggerable Launcher { get => _launcher; set => _launcher = value; }
    public float BasePlacementOffset { get => _basePlacementOffset; }
    public float PlacementOffset { get => _placementOffset; set => _placementOffset = value; }
    public float BaseLifetime { get => _baseLifetime; }

    public override void Initialize(GameObject obj)
    {
        _launcher = new WorldTransformTriggerable(this);
        _placementOffset = _basePlacementOffset;
        _additionalLifetime = 0;
    }

    public override void TriggerSpell()
    {
        _launcher.Launch();
    }

    public void ResetSpell()
    {
        _placementOffset = _basePlacementOffset;
        _additionalLifetime = 0;
    }
}