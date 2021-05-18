///
/// Author: Fred Akdogan, Daniel Cantz
/// Description: This class handels the current number of enemys aggroing the player and sends updates to its subscribers
/// ==============================================
/// Changelog:
/// 03/09/2020 - Daniel Cantz - added delegate event to notify subscribers about number changes
/// 07/09/2020 - Daniel Cantz - removed DontDestroyOnLoad - code is only commented out, since its not 100% clear why its there
/// ==============================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGameManager : MonoBehaviour
{
    public static EnemyGameManager Instance { get; private set; }

    public delegate void EnemyDelegate();
    public static event EnemyDelegate AggroNrChanged;

    public int aggroEnemys = 0;
    private int aggroBefore = 0;

    public float disableEnemyDistanceRate = 30.0f;
    private GameObject player;
    public float timer = 2f;
    private float tikker;

    private void FixedUpdate()
    {
        if (tikker >= timer)
        {
            controllAi();
            tikker = 0;
        }
        else
        {
            tikker += Time.deltaTime;
        }

    }

    private void controllAi()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemy.GetComponent<StateController>())
            {
                if (Vector3.Distance(player.transform.position, enemy.gameObject.transform.position) < disableEnemyDistanceRate)
                {
                    // Debug.Log("activate ai: " + enemy.transform);
                    enemy.GetComponent<StateController>().aiActive = true;
                }
                else
                {
                    // Debug.Log("DeActivate ai: " + enemy.transform);
                    enemy.GetComponent<StateController>().aiActive = false;
                }

            }
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(aggroEnemys != aggroBefore)
        {
            // send out delegate
            // current subscribers: MusicControll.cs
            AggroNrChanged?.Invoke();
            aggroBefore = aggroEnemys;
        }
    }
}
