using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float damage = 100;
    [SerializeField] private bool singleDamage = true;
    

    public float GetDamage()
    {
        return damage;
    }

    public void SetDamageTimes(bool singleDamage)
    {
        this.singleDamage = singleDamage;
    }

    public void Hit (Collision2D collision)
    {
        Debug.LogWarning(collision.gameObject);
        HealthObj health = collision.gameObject.GetComponent<HealthObj>();

        health.DealDamage(damage);

        if (singleDamage)
        {
            Destroy(gameObject);
        }
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    { 
        Hit(collision);
    }

}
