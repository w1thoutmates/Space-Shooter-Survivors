using UnityEngine;

[CreateAssetMenu(fileName = "NewMagnetAreaBonusModule", menuName = "Modules/Magnet Area Module")]
public class MagnetAreaBonus : Module
{
    public float magnetAreaBonus = 1f;
    public override void Apply(PlayerController player, int level)
    {
        float totalBonus = magnetAreaBonus * level;
        player.magnetBonus = player.magnetBonus + totalBonus;
        player.UpdateMagnetArea();
    }

    public override string GetBonusText(int level) => (magnetAreaBonus * level).ToString();
}
