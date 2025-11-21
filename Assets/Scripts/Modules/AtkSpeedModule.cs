using UnityEngine;

[CreateAssetMenu(fileName = "NewAtkSpeedModule", menuName = "Modules/Attack Speed Module")]
public class AtkSpeedModule : Module
{
    public float atkSpeedPercent = 0.1f;
    public override void Apply(PlayerController player, int level, ModuleQuality quality = ModuleQuality.Common)
    {
        float baseBonus = atkSpeedPercent * level;
        float finalBonus = baseBonus * ModuleQualityMultiplier.Get(quality);

        player.totalAtkSpeedMultiplier += finalBonus;
        player.fireRate = player.baseFireRate / player.totalAtkSpeedMultiplier;
    }

    public override string GetBonusText(int level, ModuleQuality quality = ModuleQuality.Common)
    {
        float baseBonus = (atkSpeedPercent * 100f) * level;
        float finalBonus = baseBonus * ModuleQualityMultiplier.Get(quality);

        return $"{finalBonus:F2}%";
    }

}
