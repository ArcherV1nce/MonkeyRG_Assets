using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public class Health
{
    private float MAX_HEALTH;
    private float health_amount;
    private float damage_reduction;
    private int recent_damage;

    public Health()
    {
        MAX_HEALTH = 100000;
        health_amount = 100;
        damage_reduction = 0;
        recent_damage = 0;
    }

    public Health(float MAX_HEALTH, float health, float damage_reduction, int recent_damage)
    {
        this.MAX_HEALTH = MAX_HEALTH;
        this.health_amount = health;
        this.damage_reduction = damage_reduction;
        this.recent_damage = recent_damage;
    }

    public Health(float health, float damage_reduction)
    {
        this.health_amount = health;
        this.damage_reduction = damage_reduction;
    }

    public Health (float MAX_HEALTH, float health, float damage_reduction)
    {
        this.MAX_HEALTH = MAX_HEALTH;
        this.health_amount = health;
        this.damage_reduction = damage_reduction;
    }

    public Health(float health, float damage_reduction, int recent_damage)
    {
        this.health_amount = health;
        this.damage_reduction = damage_reduction;
        this.recent_damage = recent_damage;
    }

    public Health(float health)
    {
        this.health_amount = health;
    }

    public void DealDamage(float damage)
    {
        float damage_dealt = damage;
        float reduction = 1f - (damage_reduction / 100); 
        damage_dealt = damage_dealt * Mathf.Clamp(reduction, 0f, 1f);
        health_amount -= damage_dealt;
        int damage_d = (int) damage_dealt;
        recent_damage += damage_d;
    }

    public float GetHealth()
    {
        return health_amount;
    }

    public float GET_MAX_HEALTH()
    {
        return MAX_HEALTH;
    }

    public void IncreaseMaxHealth(float MAX_HEALTH)
    {
        this.MAX_HEALTH = MAX_HEALTH;
    }

    public float GetRecentDamage()
    {
        return recent_damage;
    }

    public void ResetRecentDamage()
    {
        recent_damage = 0;
    }

    public void HealthAdd(float addHealthAmount)
    {
        health_amount += addHealthAmount;
        if(health_amount > MAX_HEALTH)
        {
            health_amount = MAX_HEALTH;
        }
    }

    public void SetHealth(float health_amount)
    {
        this.health_amount = health_amount;
    }

    public float GetArmorAmount()
    {
        return damage_reduction;
    }

    public void SetArmorAmount (float damage_reduction)
    {
        this.damage_reduction = damage_reduction;
    }

}
