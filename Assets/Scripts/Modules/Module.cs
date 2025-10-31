using UnityEngine;

public abstract class Module : ScriptableObject
{
    public string name;
    public string description;
    public Sprite icon;

    [HideInInspector] public float totalBonus;
    [HideInInspector] public float currentBonus;
    [HideInInspector] public int currentLevel = 1;

    public abstract void Apply(PlayerController player, int level);

    public abstract string GetBonusText(int level);
}
