using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Parameters")]
    [Range(0.1f, 3f)] [SerializeField] private float timeBetweenShots = 0.5f;
    [Range(1f, 30f)] [SerializeField] private float projectileSpeed = 15f;
    [Range(0.5f, 5f)] [SerializeField] private float projectileLifeTime = 2f;
    [SerializeField] private float projectileDamage = 100;

    [Header("Assets")]
    [SerializeField] GameObject projectilePrefab;

    //State parameters
    private float shotSpeed;
    private float shotReload;
    private List<Vector3> MuzzlePosition = new List<Vector3>();
    private Vector2 shotDirection = new Vector2 (0f, 15f);

    // Start is called before the first frame update
    private void Start()
    {
        shotReload = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateShotDirection();
        Shoot();
        ShotReload();
    }

    private void Fire()
    {
        GameObject particle = Instantiate(
        projectilePrefab,
        transform.position,
        Quaternion.identity);
        particle.GetComponent<DamageDealer>().SetDamage(projectileDamage);
        particle.GetComponent<Rigidbody2D>().velocity = shotDirection;
        Destroy(particle, projectileLifeTime);
        shotReload = timeBetweenShots;
        Debug.Log(particle.transform.position);

    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (shotReload <= 0f)
            {
                Debug.Log("Player shooting");
                Fire();
            }
        }
    }

    private void ShotReload()
    {
        shotReload -= Time.deltaTime;
    }

    private void UpdateShotDirection()
    {
        float vectorX = Input.GetAxis("Horizontal") * projectileSpeed;
        float vectorY = Input.GetAxis("Vertical") * projectileSpeed;
        shotDirection = new Vector2(vectorX, vectorY);

    }
}