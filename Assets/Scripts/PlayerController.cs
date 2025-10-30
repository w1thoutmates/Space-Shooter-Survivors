using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float playerMaxHealth;
    [HideInInspector] public float playerCurrentHealth;

    [HideInInspector] public float score;

    public float speed = 10f;
    public float tilt = 4f;

    public Transform shotSpawn;
    public ParticleSystem shotEffect;

    public float fireRate = 0.5f;
    public float nextFire = 0.0f;

    public Healthbar healthBar;

    private Camera cam;
    private Rigidbody rb;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else 
            Destroy(gameObject);
    }

    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();

        playerCurrentHealth = playerMaxHealth;
        healthBar.SetMaxHealth(playerMaxHealth);
    }

    public void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire) {
            shotEffect.Play();
            nextFire = Time.time + fireRate;
            Instantiate(R.instance.bolt, shotSpawn.transform.position, shotSpawn.rotation);
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rb.rotation = Quaternion.Euler(
            0f,
            180f,
            rb.linearVelocity.x * tilt); 

        Vector3 move = new Vector3(moveHorizontal, 0f, moveVertical) * speed;
        rb.linearVelocity = move;

        Vector3 viewPos = cam.WorldToViewportPoint(rb.position);

        viewPos.x = Mathf.Clamp(viewPos.x, 0.05f, 0.95f);
        viewPos.y = Mathf.Clamp(viewPos.y, 0.05f, 0.95f);

        Vector3 targetPos = cam.ViewportToWorldPoint(new Vector3(viewPos.x, viewPos.y, viewPos.z));
        rb.position = targetPos;

        rb.position = new Vector3(rb.position.x, 0f, rb.position.z);
    }

    public void TakeDamage(float value)
    {
        playerCurrentHealth -= value;
        healthBar.SetHealth(playerCurrentHealth);

        if (playerCurrentHealth <= 0)
        {
            Destroy(gameObject);
            Time.timeScale = 0f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        IPickable pickup = other.GetComponent<IPickable>();
        if (pickup != null)
        {
            Instantiate(R.instance.pickupEffect, other.transform.position, Quaternion.identity);
            pickup.OnPickup(this);
        }

    }

}
