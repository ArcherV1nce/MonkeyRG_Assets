using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGrid : MonoBehaviour
{
    [SerializeField] [Range(0, 1000)] private int gridSizeX = 50; 
    [SerializeField] [Range(0, 1000)] private int gridSizeY = 50;
    [SerializeField] private Grid<EnvironmentObject> worldGrid;

    [SerializeField] GameObject worldLimiter;
    


    private void Awake()
    {
        Vector3 origin_pos = new Vector3(0f, 0f, 0f);
        worldGrid = new Grid<EnvironmentObject>(gridSizeX, gridSizeY, 1f, origin_pos, (Grid<EnvironmentObject> o, int x, int y) => new EnvironmentObject(o, x, y));
    }

    private void Start()
    {
        SetBoundaries();
    }

    public class EnvironmentObject
    {
        private Grid<EnvironmentObject> grid;
        private int x;
        private int y;
        private GameObject worldObject;

        public EnvironmentObject(Grid<EnvironmentObject> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }

        public EnvironmentObject()
        {

        }

        public void AddObject(GameObject addObject)
        {
            worldObject = addObject;
            //grid.TriggerGridObjectChanged(x, y);
        }
    }

    private void SetBoundaries()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                if(x < 1 || x == gridSizeX - 1)
                {
                    Vector3 pos = new Vector3(x, y);
                    Quaternion rot = new Quaternion();
                    Instantiate(worldLimiter, pos, rot);
                    EnvironmentObject environmentObject = new EnvironmentObject();
                    environmentObject.AddObject(worldLimiter);
                    worldGrid.SetGridObject(x, y, environmentObject);
                }

                if (y < 1 || y == gridSizeY - 1)
                {
                    Vector3 pos = new Vector3(x, y);
                    Quaternion rot = new Quaternion();
                    Instantiate(worldLimiter, pos, rot);
                    EnvironmentObject environmentObject = new EnvironmentObject();
                    environmentObject.AddObject(worldLimiter);
                    worldGrid.SetGridObject(x, y, environmentObject);
                }
            }
        }
    }
}
