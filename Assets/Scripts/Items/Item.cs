using UnityEngine;


public enum ItemQuality
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
    // Cursed / Corrupted
}

public abstract class Item : ScriptableObject
{
    public string itemName;
    public ItemQuality quality;
    public string description;
    public Sprite icon;
    [HideInInspector] public int maxStackSize = 99;
    public bool isStackable = true;

    public abstract void Use(PlayerController player, int stackLevel);

}

[System.Serializable]
public class ItemInstance
{
    public Item item;
    public int count;

    public ItemInstance(Item item, int count = 1)
    {
        this.item = item;
        this.count = count;
    }

    public void Add(int amount)
    {
        count += amount;
    }

    public void Remove(int amount)
    {
        count = Mathf.Max(0, count - amount);
    }
}
