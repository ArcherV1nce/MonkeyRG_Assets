using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float damage = 100;
    

    public float GetDamage()
    {
        return damage;
    }

    public void Hit (Collision2D collision)
    {
        Debug.LogWarning(collision.transform.parent);
        Health health = collision.transform.gameObject.GetComponent<Health>();

        health.DealDamage(damage);
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    { 
        //Hit(collision);
    }

}
