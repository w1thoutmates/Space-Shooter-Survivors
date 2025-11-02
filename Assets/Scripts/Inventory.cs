using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    private List<Sprite> currentModules = new List<Sprite>(); 
    private HorizontalLayoutGroup inventoryLayout;
    private List<Modules> playerModules;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else 
            Destroy(gameObject);

        inventoryLayout = GetComponent<HorizontalLayoutGroup>();
    }

    private void Start()
    {
        playerModules = PlayerController.instance.GetComponent<PlayerModule>().ownedModules;
    }

    public void AddItemInInventoryOnUI()
    {
        foreach (var item in playerModules)
        {
            if (item?.module?.icon == null) continue;

            if (!currentModules.Contains(item.module.icon))
            {
                currentModules.Add(item.module.icon);
            }
        }

        UpdateInventoryOnUI();
    }

    public void UpdateInventoryOnUI()
    {
        int i = 0;
        foreach (Transform slot in inventoryLayout.transform)
        {
            var iconObject = slot.Find("itemIcon");
            Image icon = iconObject.GetComponent<Image>();
            TextMeshProUGUI moduleLevelText = iconObject.Find("moduleLevel").GetComponent<TextMeshProUGUI>();

            if(icon != null && moduleLevelText != null)
            {
                if (i < currentModules.Count)
                {
                    icon.sprite = currentModules[i];
                    icon.enabled = true;
                    icon.color = Color.white;
                    moduleLevelText.text = "lv." + playerModules[i].currentLevel.ToString();
                    moduleLevelText.gameObject.SetActive(true);

                }
                else
                {
                    moduleLevelText.text = "";
                    moduleLevelText.gameObject.SetActive(false);
                    icon.sprite = null;
                    icon.enabled = false;
                }
            }
            i++;
        }

    }


}
