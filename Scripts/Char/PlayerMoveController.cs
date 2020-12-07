using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    #region Config Variables

    //config variables
    [Header("Player")]
    [Range(1f, 15f)] [SerializeField] private float playerMoveSpeed = 10f;
    [Range(1f, 30f)] [SerializeField] private float playerDashMultiplier = 10f;
    [Range(0.05f, 0.3f)] [SerializeField] private float playerDashDuration = 0.1f;
    [Range(1f, 30f)] [SerializeField] private float playerDashReload = 15f;

    private float dashTimer = 0f;

    //state variables
    [Header("Debug")]
    Vector3 playerPosition;

    #endregion

    private void Awake()
    {
        
    }

    void Start()
    {

    }


    private void Update()
    {
        Move();
        PlayerDash();
        DashReload();
    }

    private void Move()
    {
        playerPosition.x = getXPos();
        playerPosition.y = getYPos();
        transform.position = playerPosition;
    }

    private float getXPos()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * playerMoveSpeed;
        var nextXPos = transform.position.x + deltaX;
        //Debug.Log(deltaX);
        return nextXPos;
    }

    private float getYPos()
    {
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * playerMoveSpeed;
        var nextYPos = transform.position.y + deltaY;
        // Debug.Log(deltaY);
        return nextYPos;
    }

    public Vector2 GetPlayerPos()
    {
        return playerPosition;
    }


    private IEnumerator PlayerDashCoroutine()
    {
        dashTimer = playerDashReload + playerDashDuration;
        playerMoveSpeed = playerMoveSpeed * playerDashMultiplier;
        yield return new WaitForSeconds(playerDashDuration);
        playerMoveSpeed = playerMoveSpeed / playerDashMultiplier;
        yield return null;
    }
    private void PlayerDash()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && dashTimer == 0)
        {
            StartCoroutine(PlayerDashCoroutine());
        }
    }

    private void DashReload()
    {
        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
            if(dashTimer < 0)
            {
                dashTimer = 0;
            }
        }
    }
}
