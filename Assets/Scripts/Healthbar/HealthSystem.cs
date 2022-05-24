using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    public int health;
    private int maxHealth;
    private float timeToRes = 2;
    private bool timerStart = false;
    
    public HealthSystem(int maxHealth) 
    {
        this.maxHealth = maxHealth;
        health = maxHealth;
    }
    public int GetHealth()
    {
        return health;
    }
    public float GetHealthPercent()
    {
        return (float)health / maxHealth;
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;

        if(health < 0)
        {
            health = 0;
        }
    }
    public void Heal(int healAmount)
    {
        health += healAmount;

        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }
    public bool RespawnTimer(bool status)
    {
        if (!status)
        {
            timerStart = true;
            timeToRes -= Time.deltaTime;

            if (timeToRes <= 0)
            {
                status = true;
                timeToRes = 5;
                health = maxHealth;
            }

        }
        return status;
    }
    public void Update(Time deltaTime)
    {

    }
    

}
