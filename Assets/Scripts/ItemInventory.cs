using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventory : MonoBehaviour
{
    public static ItemInventory instance;
    private List<ItemInstance> items = new List<ItemInstance>();
    private Dictionary<Item, GameObject> itemUIs = new Dictionary<Item, GameObject>();
    public event Action OnInventoryChanged;


    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void Add(Item item, int amount = 1)
    {
        bool had = Has(item);
        var inst = items.Find(inst => inst.item == item);

        if (inst != null)
            inst.Add(amount);
        else
        {
            inst = new ItemInstance(item, amount);
            items.Add(new ItemInstance(item));
        }

        // add item on UI

        if(!had)
        {
            R.instance.itemInventoryLayout.GetComponent<RectTransform>().sizeDelta = new Vector2(
            R.instance.itemInventoryLayout.GetComponent<RectTransform>().sizeDelta.x + 100,
            R.instance.itemInventoryLayout.GetComponent<RectTransform>().sizeDelta.y);

            var itemIcon = Instantiate(R.instance.itemImg, R.instance.itemInventoryLayout.transform, false);
            itemIcon.GetComponent<Image>().sprite = item.icon;
            itemUIs.Add(item, itemIcon);

            var text = itemIcon.transform.GetChild(0);
            if (text != null)
            {
                text.gameObject.SetActive(false);
            }
        }
        
        if(item.isStackable)
        { 
            if (inst.count > item.maxStackSize)
                inst.count = item.maxStackSize;

            if(itemUIs.TryGetValue(item, out var uiIcon))
            {
                var text = uiIcon.transform.GetChild(0);
                if (text != null)
                {
                    if (inst.count > 1)
                    {
                        text.gameObject.SetActive(true);
                        text.GetComponent<TextMeshProUGUI>().text = "x" + inst.count.ToString();
                    }
                    else
                        text.gameObject.SetActive(false);
                }
            }
        }

        item.Use(PlayerController.instance, inst.count);
        OnInventoryChanged?.Invoke();
    }

    public void Remove(Item item, int amount = 1)
    {
        var inst = items.Find(i => i.item == item);

        if (inst == null)
            return;

        // remove item from UI

        inst.Remove(amount);

        for (int i = 0; i < amount; i++)
        {
            item.OnRemove(PlayerController.instance, inst.count);
        }

        if (inst.count <= 0)
        {
            items.Remove(inst);
            if (itemUIs.TryGetValue(item, out var uiIcon))
            {
                Destroy(uiIcon); 
                itemUIs.Remove(item);
                R.instance.itemInventoryLayout.GetComponent<RectTransform>().sizeDelta = new Vector2(
                    R.instance.itemInventoryLayout.GetComponent<RectTransform>().sizeDelta.x - 100,
                    R.instance.itemInventoryLayout.GetComponent<RectTransform>().sizeDelta.y);
            }
        }
        else if (item.isStackable)
        {
            if (itemUIs.TryGetValue(item, out var uiIcon))
            {
                var text = uiIcon.GetComponentInChildren<TextMeshProUGUI>();
                if (text != null)
                {
                    if (inst.count > 1)
                    {
                        text.text = "x" + inst.count.ToString();
                    }
                    else
                    {
                        text.gameObject.SetActive(false);
                    }
                }
            }
        }
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
