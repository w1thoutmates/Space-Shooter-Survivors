using UnityEngine;

[CreateAssetMenu(fileName = "NewAtkSpeedModule", menuName = "Modules/Attack Speed Module")]
public class AtkSpeedModule : Module
{
    public float atkSpeedPercent = 0.1f;
    public override void Apply(PlayerController player, int level)
    {
        float totalBonus = atkSpeedPercent * level;

        player.fireRate = player.fireRate / (1 + totalBonus);
    }

    public override string GetBonusText(int level)
    {
        float bonus = (atkSpeedPercent * 100f) * level;
        return $"{bonus:F2}%";
    }

}
