using UnityEngine;

[CreateAssetMenu(fileName="ScoreHunter",menuName="Items/Score Hunter")]
public class ScoreHunter : Item
{
    public float scoreMultiplierBonus = 0.2f;

    public override void Use(PlayerController player, int stackLevel)
    {
        player.scoreMultiplier = 1 + (scoreMultiplierBonus * stackLevel);
    }
}
