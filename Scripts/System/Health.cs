using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    private float MAX_HEALTH;
    private float health_amoung;
    private float damage_reduction;
    private float recent_damage;


    public Health(float MAX_HEALTH, float health, float damage_reduction, float recent_damage)
    {
        this.MAX_HEALTH = MAX_HEALTH;
        this.health_amoung = health;
        this.damage_reduction = damage_reduction;
        this.recent_damage = recent_damage;
    }

    public Health(float health, float damage_reduction, float recent_damage)
    {
        this.health_amoung = health;
        this.damage_reduction = damage_reduction;
        this.recent_damage = recent_damage;
    }

    public Health(float health)
    {
        this.health_amoung = health;
    }

    public void DealDamage(float damage)
    {
        float damage_dealt = damage;
        float reduction = 1f - (damage_reduction / 100); 
        damage_dealt = damage_dealt * Mathf.Clamp(reduction, 0f, 1f);
        health_amoung -= damage_dealt;
        recent_damage += damage_dealt;
    }

    public float GetHealth()
    {
        return health_amoung;
    }

    public float GetRecentDamage()
    {
        return recent_damage;
    }

    public void ResetRecentDamage()
    {
        recent_damage = 0f;
    }

    public void HealthAdd(float addHealthAmount)
    {
        health_amoung += addHealthAmount;
        if(health_amoung > MAX_HEALTH)
        {
            health_amoung = MAX_HEALTH;
        }
    }

}
