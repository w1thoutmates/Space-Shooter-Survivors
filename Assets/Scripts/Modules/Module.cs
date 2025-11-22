using UnityEngine;

[System.Serializable]
public class Modules
{
    public Module module;
    public int currentLevel;
    public ModuleQuality quality;
    public float totalBonus;
}

public abstract class Module : ScriptableObject
{
    public string name;
    public string description;
    public Sprite icon;

    public string typeOfBonusText;
    public float bonusPerLevel;
    public bool isPercentage;

    public abstract void Apply(PlayerController player, ModuleQuality quality);

    public virtual string FormatBonus(float value)
    {
        if (isPercentage)
            return $"{(value * 100f):F2}%";
        else
            return $"{value:F2}";
    }
}
