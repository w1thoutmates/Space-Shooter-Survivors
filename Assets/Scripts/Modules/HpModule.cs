using UnityEngine;

[CreateAssetMenu(fileName = "NewHPModule", menuName = "Modules/HP Module")]
public class HpModule : Module
{
    public float healthPerLevel = 1f;

    public override void Apply(PlayerController player, int level)
    {
        float previousTotal = healthPerLevel * (level - 1);
        float bonusToAdd = healthPerLevel * level - previousTotal;

        player.playerMaxHealth += bonusToAdd;
        player.playerCurrentHealth += bonusToAdd;

        player.healthBar.SetMaxHealth(player.playerMaxHealth);
        player.healthBar.SetHealth(player.playerCurrentHealth);
    }

    public override string GetBonusText(int level) => (healthPerLevel * level).ToString();
}
