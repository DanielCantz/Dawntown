using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine.UI;
///
/// Author: ??
/// Description: Executes currently equipped Abilities & handles Abilitychanges
/// ==============================================
/// Changelog:
/// Samuel Müller - Made AbilityHandler compatable with Ability class
/// Samuel Müller - Added Syncs with inventory & hud
/// ==============================================
///
public class AbilityHandler : Singleton<AbilityHandler>
{
    //Inputmanager Names
    public string attackButtonAxisName = "Attack";
    public string defenseButtonAxisName = "Defense";
    public string bladeButtonAxisName = "Blade";
    public string barrelButtonAxisName = "Barrel";
    public string magiccoreButtonAxisName = "Magiccore";
    [SerializeField]
    private Ability _attack;
    [SerializeField] private AnimationUpdater _animationUpdater;

    public Ability Attack { get => _attack; }

    [SerializeField]
    private Ability _defense;

    public Ability Defense { get => _defense; }

    //Assign Player here
    [SerializeField] private GameObject _weaponHolder;

    public GameObject WeaponHolder { get => _weaponHolder; }

    //Spell AudioSource
    [FMODUnity.EventRef]
    private FMOD.Studio.EventInstance fmodEventAttackInst, fmodEventDefenseInst;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _attack.Spell.Initialize(_weaponHolder);
        _defense.Spell.Initialize(_weaponHolder);
        _animationUpdater = GetComponentInChildren<AnimationUpdater>();
        InventoryManager.Instance.AttackSlot.SetUpSyncs(_attack);
        InventoryManager.Instance.DefenseSlot.SetUpSyncs(_defense);
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void UpdateAbility(Ability ability, AbstractSpell selectedAbility, GameObject weaponHolder)
    {
        selectedAbility.Initialize(weaponHolder);
        ability.Spell = selectedAbility;
        _animationUpdater.RecalculateController(_attack, _defense);
    }
    public void UpdateAbility(Ability ability, Element selectedElement)
    {
        ability.Element = selectedElement;
    }
    public void UpdateAbility(Ability ability, AbstractWildcard selectedWildcard)
    {
        ability.Wildcard = selectedWildcard;
    }

    // Update is called once per frame
    void Update()
    {
        if (PopUpManager.Instance.IsGamePaused())
        {
            return;
        }

        if (!_attack.IsOnCooldown)
        {
            if (_attack.Spell.WeaponMod != WeaponEnum.neutral)
            {
                
                if (Input.GetButton(attackButtonAxisName))
                {
                    Debug.Log(_attack.Spell);
                    ButtonTriggered(attackButtonAxisName);
                    StartCoroutine(_attack.Cooldown());
                }    
            }
            
        }
        if (!_defense.IsOnCooldown)
        {
            if (_defense.Spell.WeaponMod != WeaponEnum.neutral)
            {
                if (Input.GetButton(defenseButtonAxisName))
                {
                    ButtonTriggered(defenseButtonAxisName);
                    StartCoroutine(_defense.Cooldown());
                }
            }
        }

        // Weapon switching (without inventory connection)
        if (Input.GetButtonDown(bladeButtonAxisName))
        {
            UpdateAbility(_attack, AbilityUtil.GetSpell(WeaponEnum.blade), _weaponHolder);
        }
        if (Input.GetButtonDown(barrelButtonAxisName))
        {
            UpdateAbility(_attack, AbilityUtil.GetSpell(WeaponEnum.barrel), _weaponHolder);
        }
        if (Input.GetButtonDown(magiccoreButtonAxisName))
        {
            UpdateAbility(_attack, AbilityUtil.GetSpell(WeaponEnum.magiccore), _weaponHolder);
        }

    }

    private void ButtonTriggered(string abilityAxisName)
    {
        switch (abilityAxisName)
        {
            case "Attack":
                _animationUpdater.TriggerAttack();

                fmodEventAttackInst = FMODUnity.RuntimeManager.CreateInstance(_attack.Spell.fmodEventAttack);
                FMODUnity.RuntimeManager.AttachInstanceToGameObject(fmodEventAttackInst, GetComponent<Transform>(), _rigidbody);
                fmodEventAttackInst.start();

                _attack.Spell.TriggerSpell();
                _attack.IsOnCooldown = true;
                break;
            case "Defense":
                _animationUpdater.TriggerDefense(_defense.Spell.WeaponMod == WeaponEnum.blade);

                //fmodEventDefenseInst = FMODUnity.RuntimeManager.CreateInstance(_attack.Spell.fmodEventDefense);
                //FMODUnity.RuntimeManager.AttachInstanceToGameObject(fmodEventDefenseInst, GetComponent<Transform>(), _rigidbody);
                //fmodEventDefenseInst.start();

                _defense.Spell.TriggerSpell();
                _defense.IsOnCooldown = true;
                break;
        }
    }
}