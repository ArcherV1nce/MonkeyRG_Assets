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

    [SerializeField] GameObject itemDrop;

    private void Awake()
    {
        health = new Health(100f, 0f);
        if (healthCollider == null)
        {
            healthCollider = transform.GetComponent<Collider2D>();
        }
    }

    private void Update()
    {
        CheckDeath();
    }

    public void DealDamage(float damage)
    {
        health.DealDamage(damage);
        Debug.Log("Dealt " + damage + " damage.");
        health_amount = health.GetHealth();
    }

    private void CheckDeath()
    {
        if (health_amount <= 0)
        {
            DestroyCharacter();
            if(gameObject.tag == "PlayerTag")
            {
                NextLevelStart ns = new NextLevelStart();
                ns.RestartLevel();
                //QuitApp quit = new QuitApp();
                //quit.Quit();
            }
        }
    }

    public void DestroyCharacter()
    {
        if (itemDrop != null)
        {
            Instantiate(itemDrop, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

}
