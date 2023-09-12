using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public Sprite ItemSpr;
    public string ItemName, ItemTag;
    public sbyte DropRate;

    public Item (string itemName, string itemTag,sbyte dropRate)
    {
        ItemName = itemName;
        ItemTag = itemTag;
        DropRate = dropRate;
    }
}
