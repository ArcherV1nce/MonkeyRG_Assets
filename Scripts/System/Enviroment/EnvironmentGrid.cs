using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGrid : MonoBehaviour
{
    [SerializeField] [Range(0, 1000)] private int gridSizeX = 50; 
    [SerializeField] [Range(0, 1000)] private int gridSizeY = 50;
    [SerializeField] private Grid<EnvironmentObject> worldGrid;

    [SerializeField] private bool buildingEnable = false;

    [SerializeField] private GameObject worldLimiter;
    [SerializeField] private GameObject building;
    [SerializeField] private GameObject buildingEnd;
    [SerializeField] private GameObject monkeyBuilder;

    [SerializeField] private InventoryController inventory;

    [SerializeField] private int buildingPrice = 2;

    [SerializeField] private List<GameObject> randomObjects;

    private void Awake()
    {
        Vector3 origin_pos = new Vector3(0f, 0f, 0f);
        worldGrid = new Grid<EnvironmentObject>(gridSizeX, gridSizeY, 1f, origin_pos, (Grid<EnvironmentObject> o, int x, int y) => new EnvironmentObject(o, x, y));
        inventory = FindObjectOfType<InventoryController>();
    }

    private void Start()
    {
        SetBoundaries();
        WorldGridFill();
    }

    private void Update()
    {
        Build();
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
                    Vector3 pos = worldGrid.GetGridObjectPosition(x, y);
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

    public bool UseMoneyToBuild(int cost)
    {
        Inventory inv = inventory.GetInventory();
        List<Item> items = inv.GetItemList();
        foreach (Item i in items)
        {
            if (i.itemType == Item.ItemType.Coin)
            {
                if (i.amount >= cost)
                {
                    for (int c = 0; c < cost; c++)
                    {
                        inv.UseItem(i);
                    }
                    return true;
                }
                else return false;
            }
            else return false;
        }
        return false;
    }

    public void BuildHouse()
    {
        if (UseMoneyToBuild(buildingPrice))
        {
            Vector3 buildingPos = GetMouseWorldPosition();
            Quaternion rot = new Quaternion();
            Instantiate(building, buildingPos, rot);
            EnvironmentObject environmentObject = new EnvironmentObject();
            environmentObject.AddObject(building);
            worldGrid.SetGridObject(buildingPos, environmentObject);
            Vector3 monkeyPos = new Vector3(buildingPos.x + 1f, buildingPos.y, buildingPos.z);
            GameObject monkey = Instantiate(monkeyBuilder, monkeyPos, rot);
            Destroy(monkey, 3f);
        }
    }

    public void BuildEndLevel(int price)
    {
        if (UseMoneyToBuild(price))
        {
            Vector3 buildingPos = GetMouseWorldPosition();
            Quaternion rot = new Quaternion();
            Instantiate(buildingEnd, buildingPos, rot);
            EnvironmentObject environmentObject = new EnvironmentObject();
            environmentObject.AddObject(buildingEnd);
            worldGrid.SetGridObject(buildingPos, environmentObject);
            Vector3 monkeyPos = new Vector3(buildingPos.x + 1f, buildingPos.y, buildingPos.z);
            GameObject monkey = Instantiate(monkeyBuilder, monkeyPos, rot);
            Destroy(monkey, 3f);
        }
    }

    private void Build()
    {
        if (buildingEnable)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                BuildHouse();
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                BuildEndLevel(5);
            }
        }
    }

    public float GetCellSize()
    {
        return worldGrid.GetCellSize();
    }

    public float GetHeight()
    {
        return worldGrid.GetHeight();
    }

    public float GetWidth()
    {
        return worldGrid.GetWidth();
    }

    private void WorldGridFill()
    {
        int[,] tempInt = RoomGenerator.GetRooms(gridSizeX);
        EnvironmentObject environmentObject = new EnvironmentObject();

        PlayerMoveController tempPlayer = FindObjectOfType<PlayerMoveController>();
        bool lookingForSpawn = true;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                if (tempInt[x,y] != 0)
                {
                    Vector3 pos = worldGrid.GetGridObjectPosition(x, y);
                    Quaternion rot = new Quaternion();
                    Instantiate(worldLimiter, pos, rot);
                    environmentObject.AddObject(worldLimiter);
                    worldGrid.SetGridObject(x, y, environmentObject);
                }
                else
                {
                    int i = Random.Range(0, randomObjects.Count);
                    if (randomObjects[i] != null)
                    {
                        if (lookingForSpawn)
                        {
                                Debug.Log("Looking for spawn");
                                tempPlayer.transform.position = worldGrid.GetGridObjectPosition(x, y);
                                lookingForSpawn = false;
                        }

                        else
                        {
                            Vector3 pos = worldGrid.GetGridObjectPosition(x, y);
                            Quaternion rot = new Quaternion();
                            Instantiate(randomObjects[i], pos, rot);
                            environmentObject.AddObject(randomObjects[i]);
                            worldGrid.SetGridObject(x, y, environmentObject);
                        }

                    }
                }
            }
        }
    }


}
