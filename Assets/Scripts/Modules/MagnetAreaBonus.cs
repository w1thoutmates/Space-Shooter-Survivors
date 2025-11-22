using UnityEngine;

[CreateAssetMenu(fileName = "NewMagnetAreaBonusModule", menuName = "Modules/Magnet Area Module")]
public class MagnetAreaBonus : Module
{
    public override void Apply(PlayerController player, ModuleQuality quality)
    {
        float bonusThisUpgrade = bonusPerLevel * ModuleQualityMultiplier.Get(quality);

        player.magnetBonus += bonusThisUpgrade;

        player.UpdateMagnetArea();
    }
}
