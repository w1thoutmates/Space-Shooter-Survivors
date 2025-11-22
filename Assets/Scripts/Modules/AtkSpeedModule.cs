using UnityEngine;

[CreateAssetMenu(fileName = "NewAtkSpeedModule", menuName = "Modules/Attack Speed Module")]
public class AtkSpeedModule : Module
{
    public override void Apply(PlayerController player, ModuleQuality quality)
    {
        float bonusThisUpgrade = bonusPerLevel * ModuleQualityMultiplier.Get(quality);

        player.totalAtkSpeedMultiplier += bonusThisUpgrade;

        player.fireRate = player.baseFireRate / player.totalAtkSpeedMultiplier;
    }
}
