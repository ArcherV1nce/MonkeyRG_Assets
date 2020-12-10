using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxPosX;
    [SerializeField] private float maxPosY;

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Vector2 movePos;
    [SerializeField] private bool isMoving;


    [Header("Parameters shooting")]
    [Range(0.1f, 3f)] [SerializeField] private float timeBetweenShots = 5f;
    [Range(1f, 30f)] [SerializeField] private float projectileSpeed = 15f;
    [Range(0.5f, 5f)] [SerializeField] private float projectileLifeTime = 2f;
    [SerializeField] private float projectileDamage = 50f;

    [Header("Assets")]
    [SerializeField] GameObject projectilePrefab;

    //State parameters
    private float shotSpeed;
    private float shotReload;
    private Vector2 shotDirection = new Vector2(0f, 15f);

    private void Awake()
    {

    }
    
    // Start is called before the first frame update
    void Start()
    {
        EnvironmentGrid tempGrid = FindObjectOfType<EnvironmentGrid>();
        maxPosX = tempGrid.GetCellSize() * tempGrid.GetWidth();
        maxPosY = tempGrid.GetCellSize() * tempGrid.GetHeight();
        SetRandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
        ShotReload();
    }

    private void Move()
    {
        if (isMoving)
        {
            var movementForFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards
                (transform.position, movePos, movementForFrame);

            Vector2 tempPos = new Vector2(transform.position.x, transform.position.y);
            if (tempPos == movePos)
            {
                isMoving = false;
                SetRandomPosition();
            }
        }
        else
        {
            SetRandomPosition();
        }

    }

    private void SetRandomPosition()
    {
        movePos.x = Random.Range(0, maxPosX);
        movePos.y = Random.Range(0, maxPosY);
        isMoving = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isMoving = false;
        SetRandomPosition();
    }

    private void ShotReload()
    {
        if (shotReload > 0)
        {
            shotReload -= Time.deltaTime;
        }       
    }

    private void UpdateShotDirection()
    {
        float vectorX = (movePos.x - transform.position.x) * projectileSpeed * Time.deltaTime;
        float vectorY = (movePos.y - transform.position.y) * projectileSpeed * Time.deltaTime;
        shotDirection = new Vector2(vectorX, vectorY);

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
            if (shotReload <= 0f)
            {
            UpdateShotDirection();
                Debug.Log("Enemy shooting");
                Fire();
            }
    }
}
