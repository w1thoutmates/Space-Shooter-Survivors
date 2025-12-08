using UnityEngine;


[CreateAssetMenu(fileName = "AttaractingRadar", menuName = "Items/Attaracting Radar")]
public class AttractingRadar : Item
{
    public float difficultyBonus = 0.05f;
    public override void Use(PlayerController player, int stackLevel)
    {
        player.difficulty = player.baseDifficulty + (difficultyBonus * stackLevel);
    }
}
