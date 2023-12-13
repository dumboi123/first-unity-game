using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ItemBag : MonoBehaviour
{
    public GameObject Item_form;
    public List<Item> Items= new List<Item>();
    public byte forcenumb;

    Item GetDroppedItem()
    {
        int  DropRate = Random.Range(1, 101);
        List<Item> DropableItems = new List<Item>();
        foreach (Item item in Items)
        {
            if(DropRate < item.DropRate) 
                DropableItems.Add(item);
        }
        if (DropableItems.Count > 0)
        {
            Item DroppedItem = DropableItems[Random.Range(0, DropableItems.Count)];
            return DroppedItem;
        }
        return null;
           
    }

    public void InstantiateItem(Vector3 pos)
    {
        Item DroppedItem = GetDroppedItem();
        if (DroppedItem != null)
        {
            GameObject LootGameObject = Instantiate(Item_form, pos, Quaternion.identity);
            LootGameObject.tag = DroppedItem.ItemTag;
            LootGameObject.GetComponent<Animator>().Play(DroppedItem.ItemName);

            Vector2 dropdirect = new Vector2(Random.Range(-1f, 1f), 1);
            LootGameObject.GetComponent<Rigidbody2D>().AddForce(dropdirect * forcenumb, ForceMode2D.Impulse);                
        }         
    }
}
