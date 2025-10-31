using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Expirence : MonoBehaviour
{
    public float[] expAmounts;
    private float _expAmount;

    private void Start()
    {
        int randomIndex = Random.Range(0, expAmounts.Length);
        _expAmount = expAmounts[randomIndex];
    }

    public void Collect(PlayerController player)
    {
        player.GainExp(_expAmount);
        Destroy(gameObject);
    }
}