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

    public void Hit ()
    {

    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

}
