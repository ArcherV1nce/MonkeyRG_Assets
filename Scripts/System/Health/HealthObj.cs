using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthObj : MonoBehaviour
{
    [SerializeField] private Health health;

    [SerializeField] private float health_amount = 100f;
    [SerializeField] [Range(0f, 1f)] private float armor = 0f;
    [SerializeField] private float MAX_HEALTH = 100f;
    [SerializeField] Collider2D healthCollider;

    private void Awake()
    {
        health = new Health(100f, 0f);
        if (healthCollider == null)
        {
            healthCollider = transform.GetComponent<Collider2D>();
        }
    }



}
