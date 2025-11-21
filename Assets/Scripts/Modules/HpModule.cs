using UnityEngine;

[CreateAssetMenu(fileName = "NewHPModule", menuName = "Modules/HP Module")]
public class HpModule : Module
{
    public float healthPerLevel = 1f;

    public override void Apply(PlayerController player, int level, ModuleQuality quality = ModuleQuality.Common)
    {
        float baseBonus = healthPerLevel * level;
        float finalBonus = baseBonus * ModuleQualityMultiplier.Get(quality);

        player.playerMaxHealth += finalBonus;
        player.playerCurrentHealth += finalBonus;

        player.healthBar.SetMaxHealth(player.playerMaxHealth);
        player.healthBar.SetHealth(player.playerCurrentHealth);
    }

    public override string GetBonusText(int level, ModuleQuality quality = ModuleQuality.Common)
    {
        float baseBonus = (healthPerLevel * level);
        float finalBonus = baseBonus * ModuleQualityMultiplier.Get(quality);

        return $"{finalBonus:F2}";
    }
}
