using System.Collections;
using UnityEngine;

public class ShieldPickup : MonoBehaviour, IPickable
{
    public float shieldTime;
    public float flashingTime;

    private bool isShieldActive = false;
    private float remainingTime;
    private Coroutine shieldCoroutine;

    public void OnPickup(PlayerController player)
    {
        var shield = player.GetComponent<PlayerShield>();
        if (shield != null)
            shield.ActivateShield(shieldTime, flashingTime);

        Destroy(gameObject);
    }
}
