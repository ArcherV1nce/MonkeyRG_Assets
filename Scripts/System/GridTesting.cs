using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTesting : MonoBehaviour
{

    private Grid<HeatMapGridObject> grid;

    // Start is called before the first frame update
    private void Start()
    {
        grid = new Grid<HeatMapGridObject>(100 , 100, 5f, Vector3.zero, (Grid<HeatMapGridObject> g, int x, int y) => new HeatMapGridObject(g, x, y));
    }

    private void Update()
    {
        TestWorldPos();
    }

    private void TestWorldPos()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = GetMouseWorldPosition();
            HeatMapGridObject heatMapGridObject = grid.GetGridObject(position);
            if (heatMapGridObject != null)
            {
                heatMapGridObject.AddValue(10);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            grid.GetGridObject(GetMouseWorldPosition());
        }
    }

    public class HeatMapGridObject
    {
        private const int minHeatMapValue = 0;
        private const int maxHeatMapValue = 100;

        private Grid<HeatMapGridObject> grid;
        private int x;
        private int y;
        private int value;

        public HeatMapGridObject(Grid<HeatMapGridObject> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }

        public void AddValue(int addValue)
        {
            value += addValue;
            Mathf.Clamp(value, minHeatMapValue, maxHeatMapValue);
            grid.TriggerGridObjectChanged(x,y);
        }

        public float GetValueNormalized()
        {
            return (float)value / maxHeatMapValue;
        }

        public override string ToString()
        {
            return value.ToString();
        }

    }

    public class StringGridObject
    {
        private Grid<StringGridObject> grid;
        private string letters;
        private string numbers;
        private int x;
        private int y;

        public StringGridObject(Grid<StringGridObject> grid, int x, int y)
        {

        }

        public void AddLetter(string letter)
        {
            letters += letter; 
        }

        public void AddNumber(string number)
        {
            numbers += number;
        }
    }

   /* private class HeatMapVisual
    {
        private Grid grid;

        public HeatMapVisual(Grid grid)
        {
            this.grid = grid;

            Vector3[] verticles;
            Vector2[] uv;
            int[] triangles;
            
            
            
            for (int x=0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight(); y++)
                {

                }
            }
           

        }
    }*/

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
