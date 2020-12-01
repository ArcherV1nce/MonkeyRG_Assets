using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTesting : MonoBehaviour
{

    private Grid<bool> grid;

    // Start is called before the first frame update
    private void Start()
    {
        grid = new Grid<bool>(20, 10, 5f, new Vector3(13, 0), () => new bool());
    }

    private void Update()
    {
        TestWorldPos();
    }

    private void TestWorldPos()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.SetValue(GetMouseWorldPosition(), false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            grid.GetValue(GetMouseWorldPosition());
        }
    }

    #region Mouse coursor position getter

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        Debug.Log("Mouse is at: " + vec);
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
    #endregion
}
