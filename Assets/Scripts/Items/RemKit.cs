using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[CreateAssetMenu(fileName = "RemKit", menuName = "Items/Rem Kit")]
public class RemKit : Item
{
    public float healthRegenBonus = 0.1f;
    public override void Use(PlayerController player, int stackLevel)
    {
        player.healthRegen = player.baseHealthRegen + (healthRegenBonus * stackLevel);
    }
}
