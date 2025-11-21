using UnityEngine;

[CreateAssetMenu(fileName = "NewDifficultModule", menuName = "Modules/Difficult Module")]
public class DifficultyModule : Module
{
    public float difficultBonus = 0.5f;
    public override void Apply(PlayerController player, int level, ModuleQuality quality = ModuleQuality.Common)
    {
        float baseBonus = difficultBonus * level;
        float finalBonus = baseBonus * ModuleQualityMultiplier.Get(quality);

        player.difficulty += finalBonus;
    }

    public override string GetBonusText(int level, ModuleQuality quality = ModuleQuality.Common)
    {
        float baseBonus = (difficultBonus * 100f) * level;
        float finalBonus = baseBonus * ModuleQualityMultiplier.Get(quality);

        return $"{finalBonus:F2}%";
    }
}
