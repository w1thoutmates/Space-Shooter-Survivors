using UnityEngine;

[CreateAssetMenu(fileName = "NewMoveSpeedModule", menuName = "Modules/Move Speed Module")]
public class MoveSpeedModule : Module
{
    public float moveSpeedBonus = 0.1f;
    public override void Apply(PlayerController player, int level, ModuleQuality quality = ModuleQuality.Common)
    {
        float baseBonus = moveSpeedBonus * level;
        float finalBonus = baseBonus * ModuleQualityMultiplier.Get(quality);

        player.speedMult += finalBonus;
        player.speed = player.baseSpeed * (1f + player.speedMult);

    }

    public override string GetBonusText(int level, ModuleQuality quality = ModuleQuality.Common)
    {
        float baseBonus = (moveSpeedBonus * 100f) * level;
        float finalBonus = baseBonus * ModuleQualityMultiplier.Get(quality);

        return $"{finalBonus:F2}%";
    }
}
