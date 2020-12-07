using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    private static ItemAssets instance;
    public static ItemAssets Instance
    {
        get
        {
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        instance = this;
    }
    public Transform pfItemWorld;

    public Sprite foodSprite;
    public Sprite coinSprite;
}
