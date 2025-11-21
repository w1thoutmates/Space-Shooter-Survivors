using UnityEngine;

[CreateAssetMenu(fileName = "NewLuckModule", menuName = "Modules/Luck Module")]
public class LuckModule : Module
{
    public float luckBonus = 0.5f;
    public override void Apply(PlayerController player, int level, ModuleQuality quality = ModuleQuality.Common)
    {
        float baseBonus = luckBonus * level;
        float finalBonus = baseBonus * ModuleQualityMultiplier.Get(quality);

        player.difficulty += finalBonus;
    }

    public override string GetBonusText(int level, ModuleQuality quality = ModuleQuality.Common)
    {
        float baseBonus = (luckBonus * 100f) * level;
        float finalBonus = baseBonus * ModuleQualityMultiplier.Get(quality);

        return $"{finalBonus:F2}%";
    }
}
