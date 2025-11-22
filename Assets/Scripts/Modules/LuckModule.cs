using UnityEngine;

[CreateAssetMenu(fileName = "NewLuckModule", menuName = "Modules/Luck Module")]
public class LuckModule : Module
{
    public override void Apply(PlayerController player, ModuleQuality quality)
    {
        float bonusThisUpgrade = bonusPerLevel * ModuleQualityMultiplier.Get(quality);

        player.luck += bonusThisUpgrade;
    }
}
