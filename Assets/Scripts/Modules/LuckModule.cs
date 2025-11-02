using UnityEngine;

[CreateAssetMenu(fileName = "NewLuckModule", menuName = "Modules/Luck Module")]
public class LuckModule : Module
{
    public float luckBonus = 0.5f;
    public override void Apply(PlayerController player, int level)
    {
        float totalBonus = luckBonus * level;
        player.luck = player.luck + totalBonus;
    }

    public override string GetBonusText(int level)
    {
        float bonus = (luckBonus * 100f) * level;
        return $"{bonus:F2}%";
    }
}
