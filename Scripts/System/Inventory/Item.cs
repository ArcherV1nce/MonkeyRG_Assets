using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType
    {
        HealthPotion,
        Food,
        Coin,
        Weapon,
        Material
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Food: return ItemAssets.Instance.foodSprite;
            case ItemType.Coin: return ItemAssets.Instance.coinSprite;
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Food: return false;
            case ItemType.Coin: return true;
        }
    }

}
