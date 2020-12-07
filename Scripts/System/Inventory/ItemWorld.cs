using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Debug.Log(ItemAssets.Instance);
        var transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);
        var itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);
        return itemWorld;
    }

    public static ItemWorld DropItem(Vector3 dropPosition, Item item)
    {

        Debug.Log(dropPosition.x);
        Debug.Log(dropPosition.y);
        Debug.Log(dropPosition.z);
        Vector3 randomDir = new Vector3(0,10,0);
        ItemWorld itemWorld = SpawnItemWorld(dropPosition + randomDir, item);
        return itemWorld;
    }

    private Item item;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
