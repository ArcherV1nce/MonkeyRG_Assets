using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInterController : MonoBehaviour
{

    [SerializeField] private Camera camera;

    [Header("Zoom")]
    [SerializeField] [Range(5f, 120f)] private float cameraZoom = 30f;
    [SerializeField] private static float minZoom = 5f;
    [SerializeField] private static float maxZoom = 120f;
    [SerializeField] private float zoomDelta = 5f;

    [Header("CameraMovement")]
    [SerializeField] private GameObject player;
    [SerializeField] [Range(0.5f, 30f)] private float cameraMoveSpeed = 5f;

    private void Awake()
    {
        camera = GetComponent<Camera>();
        camera.orthographicSize = cameraZoom;
    }

    void Update()
    {
        ZoomChange();
        CameraFollowPlayer(player);
        //CameraMove();
    }

    private void CameraFollowPlayer(GameObject player)
    {
        Vector3 playerPos = player.transform.position;
        playerPos.z = -10f;
         camera.transform.position = playerPos;
    }

    private void ZoomChange()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            if (cameraZoom < minZoom)
            {
                cameraZoom = minZoom;
            }
            else if (cameraZoom > maxZoom)
            {
                cameraZoom = maxZoom;
            }

            else
            {
                cameraZoom -= Input.mouseScrollDelta.y;
                camera.orthographicSize = cameraZoom;
            }

        }
    }

    #region Debug Camera Move
    /*
    private void CameraMove()
    {
        CameraMoveX();
        CameraMoveY();
    }

    private void CameraMoveY()
    {
        Vector3 cameraPos = new Vector3(camera.transform.position.x, camera.transform.position.y, -10f);

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            cameraPos.y -= cameraMoveSpeed;
            camera.transform.position = cameraPos;
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            cameraPos.y += cameraMoveSpeed;
            camera.transform.position = cameraPos;
        }
    }



    private void CameraMoveX()
    {
        Vector3 cameraPos = new Vector3(camera.transform.position.x, camera.transform.position.y, -10f);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            cameraPos.x -= cameraMoveSpeed;
            camera.transform.position = cameraPos;
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            cameraPos.x += cameraMoveSpeed;
            camera.transform.position = cameraPos;
        }
    }
    */

    #endregion
}
