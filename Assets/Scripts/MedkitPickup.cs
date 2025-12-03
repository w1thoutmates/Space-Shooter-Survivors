using UnityEngine;

public class MedkitPickup : MonoBehaviour, IPickable
{ 
    public float healValue = 1;
    
    void Start()
    {
        healValue = Mathf.RoundToInt(PlayerController.instance.playerMaxHealth / 3);
    }

    void Update()
    {
        
    }
    
    public void OnPickup(PlayerController player)
    {
        Heal(player, healValue);
        AudioManager.instance.PlaySFX(R.instance.pickupSounds[Random.Range(0, R.instance.pickupSounds.Length)]);
        Destroy(gameObject);
    }

    private void Heal(PlayerController player, float value)
    {
        player.playerCurrentHealth = Mathf.Min(player.playerCurrentHealth + value, player.playerMaxHealth);
        player.healthBar.SetHealth(player.playerCurrentHealth);
    }
}
