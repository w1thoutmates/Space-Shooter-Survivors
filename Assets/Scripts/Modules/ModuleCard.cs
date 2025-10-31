using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModuleCard : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI moduleNameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI bonusText;
    public Button selectButton;

    private Modules currentInstance;

    public void SetModule(Modules instance)
    {
        currentInstance = instance;
        Module module = instance.module;

        iconImage.sprite = module.icon;
        moduleNameText.text = module.name;
        levelText.text = "lv." + instance.currentLevel;

        string currentBonusText = module.GetBonusText(instance.currentLevel);
        string nextBonusText = module.GetBonusText(instance.currentLevel + 1);

        bonusText.text = $"{currentBonusText} -> {nextBonusText}";

        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() => OnSelect());
    }

    private void OnSelect()
    {
        PlayerModule.instance.AddModule(currentInstance.module);
        R.instance.moduleSelectionPanel.SetActive(false);

        Time.timeScale = 1f;
    }
}
