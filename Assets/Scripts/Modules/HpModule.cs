using UnityEngine;

[CreateAssetMenu(fileName = "NewHPModule", menuName = "Modules/HP Module")]
public class HpModule : Module
{
    public override void Apply(PlayerController player, ModuleQuality quality)
    {
        float bonusThisUpgrade = bonusPerLevel * ModuleQualityMultiplier.Get(quality);
        player.playerMaxHealth += bonusThisUpgrade;

        player.playerCurrentHealth += bonusThisUpgrade;

        player.healthBar.SetMaxHealth(player.playerMaxHealth);
        player.healthBar.SetHealth(player.playerCurrentHealth);
    }
}
