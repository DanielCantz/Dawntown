using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDUmmy : MonoBehaviour
{
    [SerializeField]private float health = 100f;
    private float debuffDuration = 0f;
    private float debuffDamage = 0f;
    private bool debuffed = false;


    private void Update()
    {
        if(debuffed)
        {
            debuffDuration -= Time.deltaTime;
            DoDamage(debuffDamage * Time.deltaTime);

            if (debuffDuration <= 0f)
            {
                debuffed = false;
                Debug.Log("Health: " + health);
                Debug.Log("Not Debuffed anymore");
            }
        }
    }

    public void DoDamage(float damage)
    {
        health -= damage;
        Debug.Log("revieved " + damage + "damage");
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TryToDebuff(int damage, float duration, float chance)
    {
        float randomNumber = Random.Range(0.0f, 1.0f);
        if(randomNumber < chance)
        {

            debuffed = true;
            debuffDuration = duration;
            debuffDamage = damage;
            Debug.Log("Debuffed");
        }

        
    }
}
