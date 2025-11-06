using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy stats")]
    public float enemyMaxHealth;
    private float enemyCurrentHealth;
    public float enemySpeed;

    [Header("Weapon values")]
    public float fireRate;
    public float delay;
    
    [Header("Needed resources")]
    public ParticleSystem enemyShotEffect;
    public GameObject enemyBolt;
    public Transform enemyShotSpawn;
    public Light enemyMuzzleLight;
    public TrailRenderer tr;

    [Header("Other values")] 
    public float tilt = 3f;

    [Header("Maneuver values")]
    public float xBoundary = 10f; 
    public float xPadding = 2f;
    public float maneuverSmoothTime = 0.5f; // чем меньше, тем резче.

    private float targetZ;
    private Coroutine lightCoroutine;
    private Rigidbody rb;
    private float newX = 0f;

    private bool isManeuvering = false;
    private float currentTargetX;

    private float _zStopVelocity = 0f;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemyCurrentHealth = enemyMaxHealth;
        targetZ = Random.Range(4f, 8.5f);

        InvokeRepeating("Fire", delay, fireRate);
    }


    private void Update()
    {
        if (!isManeuvering)
        {
            const float brakingDistance = 0.75f;

            if (Mathf.Abs(transform.position.z - targetZ) > brakingDistance)
            {
                rb.linearVelocity = transform.forward * -enemySpeed;
            }
            else
            {
                float newZ = Mathf.SmoothDamp(
                    transform.position.z,
                    targetZ,
                    ref _zStopVelocity,
                    0.1f
                );

                rb.MovePosition(new Vector3(transform.position.x, transform.position.y, newZ));

                if (Mathf.Abs(transform.position.z - targetZ) < 0.05f)
                {
                    rb.linearVelocity = Vector3.zero;
                    isManeuvering = true;
                    PickNewManeuverTarget();

                    if (tr != null)
                    {
                        tr.emitting = true;
                    }
                }
            }
        }
        else
        {
            newX = Mathf.Lerp(transform.position.x, currentTargetX, Time.deltaTime / maneuverSmoothTime);
            rb.MovePosition(new Vector3(newX, transform.position.y, transform.position.z));

            if (Mathf.Abs(transform.position.x - currentTargetX) < 0.35f)
            {
                StartCoroutine(DashEffect());

                PickNewManeuverTarget();
            }
        }

        float currentTiltMultiplier = 3.5f;
        float tilt = (currentTargetX - transform.position.x) * currentTiltMultiplier;
        transform.rotation = Quaternion.Euler(0, 0, -tilt);
    }

    private void PickNewManeuverTarget()
    {
        if (transform.position.x < 0)
        {
            currentTargetX = Random.Range(xBoundary - xPadding, xBoundary);
        }
        else
        {
            currentTargetX = Random.Range(-xBoundary, -xBoundary + xPadding);
        }

        StartCoroutine(DashEffect());
    }


    private void Fire()
    {
        enemyShotEffect.Play();
        StartMuzzleFlash();
        Instantiate(enemyBolt, enemyShotSpawn.position, enemyShotSpawn.rotation);
    }

    private IEnumerator DashEffect()
    {
        if (tr == null) yield break;

        tr.emitting = true;

        yield return new WaitForSeconds(1.4f);

        tr.emitting = false;
    }


    private void StartMuzzleFlash()
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

    public void TakeDamage(float value)
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
