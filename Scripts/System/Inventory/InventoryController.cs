using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private UI_Inventory uiInventory;

    private Inventory inventory;


    void Start()
    {
        inventory = new Inventory(UseItem);
        uiInventory.SetPlayer(transform.GetComponent<PlayerMoveController>());
        Debug.Log(transform.GetComponent<PlayerMoveController>());
        uiInventory.SetInventory(inventory);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse3))
        {
            List<Item> items = inventory.GetItemList();
            foreach(Item i in items)
            {
                Debug.Log(i.itemType);
                Debug.Log(i.amount);
            }
        }
    }

    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            default:
            case Item.ItemType.Food:
                {
                    //fill hungerBar
                    inventory.RemoveItem(new Item { itemType = Item.ItemType.Food, amount = 1 });
                    break;
                }

            case Item.ItemType.Coin:
                {
                    inventory.RemoveItem(new Item { itemType = Item.ItemType.Coin, amount = 1 });
                        break;
                }
        }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var itemWorld = collider.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }

    public Inventory GetInventory()
    {
        return inventory;
    }
}
