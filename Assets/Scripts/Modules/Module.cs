using UnityEngine;

[System.Serializable]
public class Modules
{
    public Module module;
    public int currentLevel;
}

public abstract class Module : ScriptableObject
{
    public string name;
    public string description;
    public Sprite icon;

    [HideInInspector] public float totalBonus;
    [HideInInspector] public float currentBonus;

    public abstract void Apply(PlayerController player, int level);

    public abstract string GetBonusText(int level);
}
