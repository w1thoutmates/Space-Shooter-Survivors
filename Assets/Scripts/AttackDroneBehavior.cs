using UnityEngine;

public class AttackDroneBehavior : MonoBehaviour
{
    [Header("Settings")]
    public float delay;
    public float fireRate;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        InvokeRepeating("Fire", delay, fireRate);
    }

    private void Fire()
    {
        //enemyShotEffect.Play();
        //StartMuzzleFlash();
        Instantiate(R.instance.bolt, transform.position, Quaternion.identity);
        AudioManager.instance.PlaySFX(R.instance.laserShotSounds[Random.Range(0, R.instance.laserShotSounds.Length)], 0.025f);
    }
}
