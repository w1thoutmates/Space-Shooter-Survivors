using UnityEngine;

[CreateAssetMenu(fileName = "NewExpBonusModule", menuName = "Modules/Expirence Bonus Module")]
public class ExpBonusModule : Module
{
    public float expGainBonus = 0.1f;
    public override void Apply(PlayerController player, int level, ModuleQuality quality = ModuleQuality.Common)
    {
        float baseBonus = expGainBonus * level;
        float totalBonus = baseBonus * ModuleQualityMultiplier.Get(quality);

        player.expMultiplier += totalBonus;
    }

    public override string GetBonusText(int level, ModuleQuality quality = ModuleQuality.Common)
    {
        float baseBonus = (expGainBonus * 100f) * level;
        float finalBonus = baseBonus * ModuleQualityMultiplier.Get(quality);

        return $"{finalBonus:F2}%";

    }
}
