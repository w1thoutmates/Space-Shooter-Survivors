using UnityEngine;

[CreateAssetMenu(fileName = "BuffedEngine", menuName = "Items/Buffed Engine")]
public class BuffedEngine : Item
{
    public float moveSpeedBonus = 0.1f;

    public override void Use(PlayerController player, int stackLevel)
    {
        player.speed = player.baseSpeed + (moveSpeedBonus * stackLevel);
    }
}
