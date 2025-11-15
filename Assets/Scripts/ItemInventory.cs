using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    public static ItemInventory instance;
    private List<ItemInstance> items = new List<ItemInstance>();
    public event Action OnInventoryChanged;

    public void Add(Item item, int amount = 1)
    {
        var inst = items.Find(inst => inst.item == item);

        if (inst != null)
            inst.Add(amount);
        else
            items.Add(new ItemInstance(item, amount)); 

        OnInventoryChanged?.Invoke();
    }

    public void Remove(Item item, int amount = 1)
    {
        var inst = items.Find(inst => inst.item == item);

        if (inst == null)
            return;

        inst.Remove(amount);

        if (inst.count <= 0)
            items.Remove(inst);

        OnInventoryChanged?.Invoke();
    }

    public IReadOnlyList<ItemInstance> GetAllItems() => items;

    public bool Has(Item item)
    {
        return items.Exists(inst => inst.item == item);
    }

    public int GetCount(Item item)
    {
        var inst = items.Find(inst => inst.item == item);
        return inst?.count ?? 0;
    }

    public void ApplyAll(PlayerController player)
    {
        foreach (var inst in items)
        {
            inst.item.Use(player, inst.count);
        }
    }

}
