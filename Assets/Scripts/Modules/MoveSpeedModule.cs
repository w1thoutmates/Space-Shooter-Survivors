using UnityEngine;

[CreateAssetMenu(fileName = "NewMoveSpeedModule", menuName = "Modules/Move Speed Module")]
public class MoveSpeedModule : Module
{
    public float moveSpeedBonus = 0.1f;
    public override void Apply(PlayerController player, int level)
    {
        float totalBonus = 1f + moveSpeedBonus * level;
        player.speed = player.baseSpeed * totalBonus;
    }

    public override string GetBonusText(int level)
    {
        float bonus = (moveSpeedBonus * 100f) * level;
        return $"{bonus:F2}%";
    }
}
