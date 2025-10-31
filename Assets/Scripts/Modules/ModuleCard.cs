using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ModuleCard : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI moduleNameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI bonusText;
    public Button selectButton;

    private Module currentModule;

    public void SetModule(Module module)
    {
        currentModule = module;

        iconImage.sprite = module.icon;
        moduleNameText.text = module.name;
        levelText.text = "Lv." + module.currentLevel;

        string currentBonusText = module.GetBonusText(module.currentLevel);
        string nextBonusText = module.GetBonusText(module.currentLevel + 1);

        bonusText.text = $"{currentBonusText} -> {nextBonusText}";

        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() => OnSelect());
    }

    private void OnSelect()
    {
        PlayerModule.instance.AddModule(currentModule);

        R.instance.moduleSelectionPanel.SetActive(false);
    }
}
