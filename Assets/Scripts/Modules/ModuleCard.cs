using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public enum ModuleQuality 
{
    Common,
    Uncommon, 
    Rare,
    Epic, 
    Legendary 
}

public class ModuleCard : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI moduleNameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI bonusText;
    public TextMeshProUGUI qualityText;
    public Button selectButton;
    public Image fillImage;
    public Image iconFillImage;
    [HideInInspector] public ModuleQuality moduleQuality;

    private static readonly Dictionary<ModuleQuality, float> upgradeQualityChances = new Dictionary<ModuleQuality, float>()
    {
        { ModuleQuality.Common, 0.3f },
        { ModuleQuality.Uncommon, 0.15f },
        { ModuleQuality.Rare, 0.05f },
        { ModuleQuality.Epic, 0.01f },
        { ModuleQuality.Legendary, 0f }
    };

    private Modules currentInstance;

    public void SetModule(Modules instance)
    {
        currentInstance = instance;
        Module module = instance.module;

        iconImage.sprite = module.icon;
        moduleNameText.text = module.name;
        levelText.text = "lv." + instance.currentLevel;

        bool isNew = !PlayerModule.instance.HasModule(module);

        if (isNew)
        {
            fillImage.color = new Color(51f / 255f, 49f / 255f, 50f / 255f, 1f); // #333132
            iconFillImage.color = new Color(114f / 255f, 129f / 255f, 134f / 255f, 1f); // #728186
        }
        else
        {
            moduleQuality = RollQuality();
            UpdateFillColorDependsOnQuality(moduleQuality);
        }

        instance.quality = moduleQuality;

        string currentBonusText = module.GetBonusText(instance.currentLevel);
        string nextBonusText = module.GetBonusText(instance.currentLevel + 1);

        bonusText.text = $"<color=#F2FF56>{module.typeOfBonusText}</color>: <color=\"Grey\">{currentBonusText}</color> -> <color=\"Green\">{nextBonusText}</color>";

        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() => OnSelect());
    }

    private void OnSelect()
    {
        PlayerModule.instance.GetComponent<PlayerModule>().AddModule(currentInstance.module);
        // в зависимости от качества модуля изменять прибавляемый бонус модуля
        Inventory.instance.AddItemInInventoryOnUI();

        PlayerController.instance.expirenceBar.StopRainbow();
        PlayerController.instance.magnetArea.GetComponent<Magnet>().gameObject.SetActive(true);
        Time.timeScale = 1f;
    }

    private void Start()
    {
        //moduleQuality = RollQuality();
        //UpdateFillColorDependsOnQuality(moduleQuality);
    }

    public ModuleQuality RollQuality() 
    {
        ModuleQuality current = ModuleQuality.Common;

        while (true)
        {
            float rollChance = upgradeQualityChances[current];
            if (rollChance <= 0f)
                return current;

            float roll = Random.value;

            if (roll <= rollChance)
            {
                current = NextRarity(current);
            }
            else
                return current;
        }
    }

    private static ModuleQuality NextRarity(ModuleQuality mq)
    {
        switch (mq)
        {
            case ModuleQuality.Common: return ModuleQuality.Uncommon;
            case ModuleQuality.Uncommon: return ModuleQuality.Rare;
            case ModuleQuality.Rare: return ModuleQuality.Epic;
            case ModuleQuality.Epic: return ModuleQuality.Legendary;
            default: return ModuleQuality.Legendary;
        }
    }

    public void UpdateFillColorDependsOnQuality(ModuleQuality quality)
    {
        switch(quality)
        {
            // new Module color: fillImage.color = new Color(51f, 49f, 50f, 255f); // #333132

            case ModuleQuality.Common: 
                fillImage.color = new Color(5f / 255f, 32f / 255f, 15f / 255f, 1f); // #05200F
                iconFillImage.color = new Color(34f / 255f, 141f / 255f, 77f / 255f, 1f); // #228D4D
                qualityText.text = quality.ToString();
                break;
            case ModuleQuality.Uncommon:
                fillImage.color = new Color(1f / 255f, 34f / 255f, 51f / 255f, 1f); // #012233
                iconFillImage.color = new Color(7f / 255f, 93f / 255f, 133f / 255f, 1f); // #075D85 
                qualityText.text = quality.ToString();
                break;
            case ModuleQuality.Rare:
                fillImage.color = new Color(51f / 255f, 0f / 255f, 51f / 255f, 1f); // #330033
                iconFillImage.color = new Color(143f / 255f, 3f / 255f, 135f / 255f, 1f); // #8F0387
                qualityText.text = quality.ToString();
                break;
            case ModuleQuality.Epic:
                fillImage.color = new Color(35f / 255f, 5f / 255f, 7f / 255f, 1f); // #230507
                iconFillImage.color = new Color(143f / 255f, 24f / 255f, 14f / 255f, 1f); // #8F180E
                qualityText.text = quality.ToString();
                break;
            case ModuleQuality.Legendary:
                fillImage.color = new Color(72f / 255f, 62f / 255f, 1f / 255f, 1f); // #483E01
                iconFillImage.color = new Color(241f / 255f, 223f / 255f, 13f / 255f, 1f); // #F1DF0D
                qualityText.text = quality.ToString();
                break;
        }
    }
}

public static class ModuleQualityMultiplier
{
    private static readonly Dictionary<ModuleQuality, float> multipliers =
        new Dictionary<ModuleQuality, float>()
        {
            { ModuleQuality.Common,    1.00f },
            { ModuleQuality.Uncommon,  1.10f },
            { ModuleQuality.Rare,      1.20f },
            { ModuleQuality.Epic,      1.40f },
            { ModuleQuality.Legendary, 2.00f }
        };

    public static float Get(ModuleQuality q) => multipliers[q];
}

