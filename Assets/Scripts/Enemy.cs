using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Enemy stats")]
    public float enemyMaxHealth;
    protected float enemyCurrentHealth;
    public float enemySpeed;

    [Header("Weapon values")]
    public float fireRate;
    public float delay;

    [Header("Needed resources")]
    public ParticleSystem enemyShotEffect;
    public GameObject enemyBolt;
    public Transform enemyShotSpawn;
    public Light enemyMuzzleLight;

    [Header("Other values")]
    public float tilt = 3f;

    private Coroutine lightCoroutine;
    protected Rigidbody rb;


    protected virtual void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
        rb = GetComponent<Rigidbody>();
        InvokeRepeating("Fire", delay, fireRate);
    }

    private void Fire()
    {
        enemyShotEffect.Play();
        StartMuzzleFlash();
        Instantiate(enemyBolt, enemyShotSpawn.position, enemyShotSpawn.rotation);
    }

    protected void StartMuzzleFlash()
    {
        if (lightCoroutine != null) StopCoroutine(lightCoroutine);
        enemyMuzzleLight.gameObject.SetActive(true);
        enemyMuzzleLight.intensity = 45f;
        lightCoroutine = StartCoroutine(FadeLight());
    }

    private IEnumerator FadeLight()
    {
        yield return new WaitForSeconds(0.1f * 0.3f);
        float fadeTime = 0.15f * 0.7f;
        float startIntensity = enemyMuzzleLight.intensity;
        float timer = 0;

        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            enemyMuzzleLight.intensity = Mathf.Lerp(startIntensity, 0f, timer / fadeTime);
            yield return null;
        }
        enemyMuzzleLight.gameObject.SetActive(false);
    }

    public virtual void TakeDamage(float value)
    {
        var hitEffect = Instantiate(R.instance.lazerRayHit, transform.position, Quaternion.identity);
        hitEffect.transform.SetParent(transform, true);

        enemyCurrentHealth -= value;

        if (enemyCurrentHealth <= 0)
        {
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
            return;
        }
    }

    public bool IsDead() => enemyCurrentHealth <= 0;
}
