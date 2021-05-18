
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
///
/// Author: Samuel Müller: sm184
/// Description: Basic health system
/// ==============================================
/// Changelog:  
/// fa019 - Changed ints to floats gives maxHealth Setter
/// dc029 - Death now loads the starting area
/// ==============================================
///
public abstract class UnitHasHealth : MonoBehaviour
{
    [SerializeField]
    private float _health = 0;

    public float CurrentHealth { get => _health; }

    private bool deathAlreadyTriggered = false;

    [SerializeField]
    private float _maxHealth = 100;
    public float MaxHealth
    {
        get => _maxHealth;
        set
        {
            _maxHealth = value;
        }
    }


    public float Health { get => _health; }

    protected virtual void Start()
    {
        _health = _maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        _health = Mathf.Clamp(_health - damage, 0, _maxHealth);
        //takeDamageSoundHere
        if (_health == 0)
        {
            if (!CompareTag("Player"))
            {
                if (!deathAlreadyTriggered)
                {
                    deathAlreadyTriggered = true;
                    OnDeath();
                }
                Destroy(gameObject);
            }
            else
            {
                OnPlayerDeath();
            }
            //deathSoundHere
        }
    }

    protected virtual void OnPlayerDeath()
    {
        // trigger death Screen
        InventoryManager.Instance.DeathScreen.gameObject.SetActive(true);
        UIFader.Instance.UiElement = InventoryManager.Instance.DeathScreen;
        UIFader.Instance.FadeIn();
        Invoke("LoadStartingArea", 5);
    }

    private void LoadStartingArea()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //dont destroy inventory
        GameObject hud = GameObject.FindGameObjectWithTag("HUD");
        Destroy(player);
        Destroy(hud);
        SceneManager.LoadScene("StartingArea");
    }

    protected virtual void OnDeath()
    {
        Debug.Log(transform.name + " died");
    }

    public virtual void Heal(float healValue)
    {
        _health = Mathf.Clamp(_health + healValue, 0, _maxHealth);
        Debug.Log("Player healed");
        //healSoundHere
    }
}
