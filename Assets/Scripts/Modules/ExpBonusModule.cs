using UnityEngine;

[CreateAssetMenu(fileName = "NewExpBonusModule", menuName = "Modules/Expirence Bonus Module")]
public class ExpBonusModule : Module
{
    public float expGainBonus = 0.1f;
    public override void Apply(PlayerController player, int level)
    {
        float totalBonus = expGainBonus * level;

        player.expMultiplier = player.expMultiplier + totalBonus;
    }

    public override string GetBonusText(int level)
    {
        float bonus = (expGainBonus * 100f) * level;

        return $"{bonus:F2}%";

    }
}
