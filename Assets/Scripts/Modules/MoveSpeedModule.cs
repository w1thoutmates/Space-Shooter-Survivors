using UnityEngine;

[CreateAssetMenu(fileName = "NewMoveSpeedModule", menuName = "Modules/Move Speed Module")]
public class MoveSpeedModule : Module
{
    public override void Apply(PlayerController player, ModuleQuality quality)
    {
        float bonusThisUpgrade = bonusPerLevel * ModuleQualityMultiplier.Get(quality);

        player.speed += bonusThisUpgrade;
    }
}
