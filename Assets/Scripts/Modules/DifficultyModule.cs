using UnityEngine;

[CreateAssetMenu(fileName = "NewDifficultModule", menuName = "Modules/Difficult Module")]
public class DifficultyModule : Module
{
    public override void Apply(PlayerController player, ModuleQuality quality)
    {
        float bonusThisUpgrade = bonusPerLevel * ModuleQualityMultiplier.Get(quality);

        player.difficulty += bonusThisUpgrade;
    }
}
