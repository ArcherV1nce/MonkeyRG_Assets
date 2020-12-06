using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGrid : MonoBehaviour
{
    [SerializeField] [Range(0, 1000)] private int gridSizeX = 50; 
    [SerializeField] [Range(0, 1000)] private int gridSizeY = 50;
    [SerializeField] private Grid<GameObject> worldGrid;


    private void Awake()
    {
        Vector3 origin_pos = new Vector3(0f, 0f, -10f);
        worldGrid = new Grid<GameObject>(gridSizeX, gridSizeY, 1f, origin_pos);
    }
}
