using UnityEngine;

[CreateAssetMenu(fileName = "NewDifficultModule", menuName = "Modules/Difficult Module")]
public class DifficultyModule : Module
{
    public float difficultBonus = 0.5f;
    public override void Apply(PlayerController player, int level)
    {
        float totalBonus = difficultBonus * level;
        player.difficulty = player.luck + totalBonus;
    }

    public override string GetBonusText(int level)
    {
        float bonus = (difficultBonus * 100f) * level;
        return $"{bonus:F2}%";
    }
}
