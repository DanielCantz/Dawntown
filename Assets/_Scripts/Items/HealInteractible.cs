using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// Author: Samuel Müller: sm184
/// Description: Interactible which eather heals the player instantly or applies a regeneration buff.
/// ==============================================
/// Changelog: 
/// ==============================================
///

public class HealInteractible : Interactible
{
    private StatHandler _statHandler;

    private BuffHandler _buffHandler;

    [Tooltip("Defines wether this Interactible instantly heals the player or applies a regeneration buff.")]
    [SerializeField] private bool _appliesRegeneration = false;

    [Tooltip("The lifetime of the regeneration buff applied to the player after interacting with this. (Only essential when appliedRegeneration == true)")]
    [SerializeField] private float _regenerationLifetime = 0;

    [Tooltip("The value the player heals, when interacting with this. If this adds a regeneration buff, this value will be healed every tick.")]
    [SerializeField] private float _healValue;

    override protected void Awake()
    {
        base.Awake();
        _buffHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<BuffHandler>();
        _statHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<StatHandler>();
        UpdateSprite(AbilityUtil.GetAbilityHUDElement(ElementEnum.heal).Sprite);
    }

    public override void Interact()
    {
        if (_statHandler.CurrentHealth < _statHandler.MaxHealth)
        {
            if (_appliesRegeneration)
            {
                _buffHandler.AddBuff(new Regenerate(_statHandler, new Stat(_regenerationLifetime), _healValue));
            } else
            {
                _statHandler.Heal(_healValue);
            }
            Destroy(gameObject);
        }
    }
}
