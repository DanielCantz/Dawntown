using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class MusicControl : MonoBehaviour
{
    public EliasPlayer eliasPlayer;

    private StatHandler playerStatHandler;
    private float playerHealthPercent = 100;
    private float playerHealthWhenEnemiesApproach = 100;
    private string lastCalledActionPreset = "Peace";
    private string currentStateOfWar = "Peace";
    private int nrOfEnemies = 0;
    private float elapsedTime = 0f;
    private bool firstDamage = false;
    float intensity = 0f;
    int intIntensity = 0;

    void OnEnable()
    {
        EnemyGameManager.AggroNrChanged += HandleAggroCount;
    }
    void OnDisable()
    {
        EnemyGameManager.AggroNrChanged -= HandleAggroCount;
    }
    private void Start()
    {
        playerStatHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<StatHandler>();
    }

    //this function gets called externally every time the number of enemies currently targeting the player changes
    private void HandleAggroCount()
    {
        nrOfEnemies = EnemyGameManager.Instance.aggroEnemys;
        //print("Enemies: " + nrOfEnemies);
    }

    void Update()
    {
        //call musicCheck() only every quarter second
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= .25f)
        {
            elapsedTime = 0f;
            musicCheck();
        }
    }

    private void musicCheck()
    {
        playerHealthPercent = (playerStatHandler.Health / playerStatHandler.MaxHealth) * 100;

        //check if war is imminent (enemies targeting player)
        if (currentStateOfWar == "Peace")            
        {
            if (nrOfEnemies > 1 && playerHealthPercent > 0)
            {
                currentStateOfWar = "War";
                print("Music: go to war");
                eliasPlayer.RunActionPreset("Enemies Approach", true);
                lastCalledActionPreset = "Enemies Approach";
                playerHealthWhenEnemiesApproach = playerHealthPercent;
            }
        }

        if (currentStateOfWar == "War")
            
        {
            //check if Peace if imminent (PC killed everyone)
            if (nrOfEnemies <= 0 && playerHealthPercent > 0)
            {
                currentStateOfWar = "Peace";
                print("Music: go to peace");
                firstDamage = false;

                // trigger only if preset is not already running, to avoid unnessessary retriggering
                if (lastCalledActionPreset != "Peace")
                {
                    eliasPlayer.RunActionPreset("Peace", true);
                    lastCalledActionPreset = "Peace";
                }
            }

            //check if Death (PC died)
            else if (playerHealthPercent <= 0)
            {
                currentStateOfWar = "Player Death";
                print("Music: player death");

                // trigger only if preset is not already running, to avoid unnessessary retriggering
                if (lastCalledActionPreset != "Death")
                {
                    eliasPlayer.RunActionPreset("Death", true);
                    lastCalledActionPreset = "Death";
                }
            }

            //if PC gets damaged for the first time since enemies approached, trigger next ActionPreset once
            else if (!firstDamage && playerHealthPercent < playerHealthWhenEnemiesApproach)
            {
                firstDamage = true;

                // trigger only if preset is not already running, to avoid unnessessary retriggering
                if (lastCalledActionPreset != "Battle 1")
                {
                    eliasPlayer.RunActionPreset("Battle 1", true);
                    lastCalledActionPreset = "Battle 1";                    
                    print("Music: first damage / Battle 1");
                }
            }

            //trigger higher Battle levels 
            else
            {
                //calculate a variable intensity expressing how intense this game feels, using nr of enemies and pc health
                //intensity = ((nrOfEnemies * 10) + (100 - playerHealthPercent)) / 25;
                //intensity = Math.Floor(intensity);
                intIntensity = clamp((nrOfEnemies + ((100 - playerHealthPercent) / 25)), 1, 4);
                //print("Music: intIntensity = " + intIntensity + ", Health = " + playerHealthPercent + ", Enemies = " + nrOfEnemies);


                switch (intIntensity)
                {
                    case 1:
                        // trigger only if preset is not already running, to avoid unnessessary retriggering
                        if (lastCalledActionPreset != "Battle 1")
                        {
                            eliasPlayer.RunActionPreset("Battle 1", true);
                            lastCalledActionPreset = "Battle 1";
                            print("Music: Battle 1");
                        }
                        break;
                    case 2:
                        // trigger only if preset is not already running, to avoid unnessessary retriggering
                        if (lastCalledActionPreset != "Battle 2")
                        {
                            eliasPlayer.RunActionPreset("Battle 2", true);
                            lastCalledActionPreset = "Battle 2";
                            print("Music: Battle 2");
                        }
                        break;
                    case 3:
                        // trigger only if preset is not already running, to avoid unnessessary retriggering
                        if (lastCalledActionPreset != "Battle 3")
                        {
                            eliasPlayer.RunActionPreset("Battle 3", true);
                            lastCalledActionPreset = "Battle 3";
                            print("Music: Battle 3");
                        }
                        break;
                    case 4:
                        // trigger only if preset is not already running, to avoid unnessessary retriggering
                        if (lastCalledActionPreset != "Battle 4")
                        {
                            eliasPlayer.RunActionPreset("Battle 4", true);
                            lastCalledActionPreset = "Battle 4";
                            print("Music: Battle 4");
                        }
                        break;
                    default:
                        print("Music: default switch case");
                        break;
                }
            }

        }
    }

    private int clamp(float number, int min, int max)
    {
        int numberInt = (int)number;
        
        if (numberInt.CompareTo(max) > 0)
        {
            return max;
        }
        if (numberInt.CompareTo(min) < 0)
        {
            return min;
        }
        return numberInt;
    }
}
