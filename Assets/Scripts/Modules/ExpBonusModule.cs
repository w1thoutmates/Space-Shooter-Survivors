using UnityEngine;

[CreateAssetMenu(fileName = "NewExpBonusModule", menuName = "Modules/Expirence Bonus Module")]
public class ExpBonusModule : Module
{
    public override void Apply(PlayerController player, ModuleQuality quality)
    {
        float bonusThisUpgrade = bonusPerLevel * ModuleQualityMultiplier.Get(quality);

        player.expMultiplier += bonusThisUpgrade;
    }
}
