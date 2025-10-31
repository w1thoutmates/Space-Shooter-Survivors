using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float playerMaxHealth;
    [HideInInspector] public float playerCurrentHealth;

    [HideInInspector] public float score;
    public float luck;

    public Collider magnetArea;
    public float magnetBonus;

    public float speed = 10f;
    public float tilt = 4f;

    public Transform shotSpawn;
    public ParticleSystem shotEffect;

    public float fireRate = 0.5f;
    public float nextFire = 0.0f;

    public Healthbar healthBar;
    public ExpirenceBar expirenceBar;

    [HideInInspector] public float level = 1;
    public float currentExp;
    public float maxExp;
    public float expMultiplier = 1f;

    private Camera cam;
    private Rigidbody rb;

    public Transform moduleGridContainer;

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

        var magnetScale = magnetArea.transform.localScale;
        magnetArea.transform.localScale = new Vector3(
            magnetScale.x + magnetBonus,
            magnetScale.y + magnetBonus,
            magnetScale.z + magnetBonus
        );

        playerCurrentHealth = playerMaxHealth;
        healthBar.SetMaxHealth(playerMaxHealth);

        expirenceBar.SetMaxExp(maxExp);
        expirenceBar.SetExp(currentExp);

        level = 1;
        R.instance.levelText.text = "lv." + level.ToString();
    }

    public void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire) {
            shotEffect.Play();
            nextFire = Time.time + fireRate;
            Instantiate(R.instance.bolt, shotSpawn.transform.position, shotSpawn.rotation);
        }

        if (Input.GetKeyDown(KeyCode.L))
            GainExp(maxExp);
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
        if (GetComponent<PlayerShield>().IsActive())
        {
            GameController.instance.AddScore(10);
            return;
        }

        playerCurrentHealth -= value;
        healthBar.SetHealth(playerCurrentHealth);

        if (playerCurrentHealth <= 0)
        {
            if (gameObject != null)
            {
                Destroy(gameObject);
                Time.timeScale = 0f;
            }
            return;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("expirence")) return;

        IPickable pickup = other.GetComponent<IPickable>();
        if (pickup != null)
        {
            Instantiate(R.instance.pickupEffect, other.transform.position, Quaternion.identity);
            pickup.OnPickup(this);
        }

    }

    public void GainExp(float exp)
    {
        currentExp += exp * expMultiplier;

        if (currentExp >= maxExp)
        {
            currentExp -= maxExp;
            level++;
            maxExp *= 1.25f;

            expirenceBar.SetMaxExp(maxExp);
            R.instance.levelText.text = "lv." + level;

            ShowModuleChoices();

            magnetArea.GetComponent<Magnet>().enabled = false;
            Time.timeScale = 0;

            R.instance.moduleSelectionPanel.SetActive(true);

            SpawnLevelUpText();
        }

        expirenceBar.SetExp(currentExp);
    }

    private void ShowModuleChoices()
    {
        List<Module> choices = PlayerModule.instance.GetModuleChoices();

        foreach (Transform child in moduleGridContainer)
            Destroy(child.gameObject);

        foreach (var module in choices)
        {
            GameObject cardObj = Instantiate(R.instance.moduleCard, moduleGridContainer);
            ModuleCard card = cardObj.GetComponent<ModuleCard>();

            Modules instance = PlayerModule.instance.GetInstance(module);
            card.SetModule(instance);
        }

        R.instance.moduleSelectionPanel.SetActive(true);
    }

    private void SpawnLevelUpText()
    {
        if (R.instance.levelUpText != null)
        {
            Vector3 spawnPos = transform.position + Vector3.up * 2f;

            var levelupText = Instantiate(R.instance.levelUpText, spawnPos, Quaternion.identity);

            var uiParent = GameObject.Find("PopupUI").transform;
            levelupText.transform.SetParent(uiParent, false);
            levelupText.transform.SetAsFirstSibling();
        }
    }

    // TODO: queue for choices.

}
