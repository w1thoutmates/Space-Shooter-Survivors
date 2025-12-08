using UnityEngine;


[CreateAssetMenu(fileName = "LuckyMark", menuName = "Items/Lucky Mark")]
public class LuckyMark : Item
{
    public float luckBonus = 0.05f;
    public override void Use(PlayerController player, int stackLevel)
    {
        player.luck = player.baseLuck + (luckBonus * stackLevel);
    }
}
