using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

///
/// Author: Fred Newton, Akdogan : fa019
/// Description: Enemy Stat Handler that should be the inface where the OtherScripts get access to object stats
/// ==============================================
/// Changelog: 
/// 07/09/2020 - Daniel Cantz - added onDeath exception for starting area
/// 07/09/2020 - Daniel Cantz - added Delegate to inform Starting area if a dummy was destroyed
/// ==============================================
///
public class EnemyStatHandler : StatHandler
{
    private StateController controller;
    [HideInInspector] public GameObject target;
    [HideInInspector] public Vector3 IdleArea;
    [HideInInspector] public GameObject FirePoint;
    [HideInInspector] public GameObject shield;
    [HideInInspector] public GameObject sword;
    [HideInInspector] public float currentBulletAmount;
    [HideInInspector] public bool isAttacking;
    [SerializeField] private DropManager _dropManager;

    // for Starting Area Only
    public delegate void DummyDestroyedDelegate();
    public static event DummyDestroyedDelegate DummyWasDestroyed;

    NPC_HitSound NPC_HitSoundComponent;

    public override void TakeDamage(float damage)
    {
        StartCoroutine(redFlashEffect());
        base.TakeDamage(damage);
        if (damage > 3)
        {
            NPC_HitSoundComponent = this.GetComponent<NPC_HitSound>();
            NPC_HitSoundComponent.triggerHitSound();
            print("Audio: enemy takes damage " + damage);
        }
        
        
    }

    IEnumerator redFlashEffect()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        controller = gameObject.GetComponent<StateController>();
        target = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < controller.transform.childCount; i++)
        {
            if (controller.transform.GetChild(i).name.Equals("FirePoint"))
            {
                FirePoint = controller.transform.GetChild(i).gameObject;
            }
        }

        foreach (Transform child in gameObject.transform) if (child.CompareTag("Shield"))
            {
                shield = child.gameObject;
                shield.GetComponent<Shield>().duration = controller.enemyStats.duration;
                shield.GetComponent<Shield>().element = controller.enemyStats.element;
                shield.GetComponent<Shield>().slowAmount = controller.enemyStats.slowAmount;
                shield.GetComponent<Shield>().damage = controller.enemyStats.damage;
            }

        foreach (Transform child in gameObject.transform) if (child.CompareTag("Sword"))
            {
                sword = child.gameObject;
                sword.GetComponent<Sword>().duration = controller.enemyStats.duration;
                sword.GetComponent<Sword>().element = controller.enemyStats.element;
                sword.GetComponent<Sword>().slowAmount = controller.enemyStats.slowAmount;
                sword.GetComponent<Sword>().damage = controller.enemyStats.damage;
            }
    }

    protected override void OnDeath()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "StartingArea")
        {
            // send out delegate
            // current subscribers: StartingRoomManager.cs
            DummyWasDestroyed?.Invoke();
            return;
        }
        EnemyGameManager.Instance.aggroEnemys--;
        _dropManager.DropItem(this);
        base.OnDeath();
    }
}
