using UnityEngine;

public class Expirence : MonoBehaviour
{
    public float[] expAmounts;
    private float _expAmount;

    private void Start()
    {
        int randomIndex = Random.Range(0, expAmounts.Length);
        _expAmount = expAmounts[randomIndex];

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
    }

    public void Collect(PlayerController player)
    {
        if(player == null || player.gameObject == null) return;

        AudioManager.instance.PlaySFX(R.instance.collectExpSound, 0.015f);

        player.GainExp(_expAmount);
        Destroy(gameObject);
    }
}