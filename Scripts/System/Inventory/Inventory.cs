using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory 
{
    public event System.EventHandler OnItemListChanged;
    public List<Item> itemList;
    public Action<Item> useActionItem;

    public Inventory(Action<Item> useActionItem)
    {
        this.useActionItem = useActionItem;
        itemList = new List<Item>();

        itemList.Add(new Item { itemType = Item.ItemType.Coin, amount = 1});
    }

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            bool alreadyInInventory = false;
            foreach (var invItem in itemList)
            {
                if (invItem.itemType == item.itemType)
                {
                    invItem.amount += item.amount;
                    alreadyInInventory = true;
                }
            }
            if (!alreadyInInventory)
            {
                itemList.Add(item);
            }
        }
        else
        {
            itemList.Add(item);
        }
        OnItemListChanged?.Invoke(this, System.EventArgs.Empty);
    }

    public void RemoveItem(Item item)
    {
        if (item.IsStackable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <= 0)
            {
                itemList.Remove(itemInInventory);
            }
        }
        else
        {
            itemList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, System.EventArgs.Empty);
    }

    public void UseItem(Item item)
    {
        useActionItem(item);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
