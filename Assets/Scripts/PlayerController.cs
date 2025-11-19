using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.LowLevelPhysics;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Player stats")]
    public float playerMaxHealth;
    [HideInInspector] public float playerCurrentHealth;
    [HideInInspector] public float score;
    [HideInInspector] public float scoreMultiplier = 1.0f;
    public float luck;
    public float difficulty = 0.15f;
    [HideInInspector] public float magnetBonus;
    [HideInInspector] public float baseMagnetBonus;
    [HideInInspector] public float pickupMagnetBonus = 0;
    public float speed = 10f;
    [HideInInspector] public float baseSpeed;
    public float fireRate = 0.5f;
    [HideInInspector] public float baseFireRate;
    [HideInInspector] public float nextFire = 0.0f;
    [HideInInspector] public float level = 1;
    public float currentExp;
    public float maxExp;
    public float expMultiplier = 1f;
    public float invincibilityLength;
    public float flashLength = 0.1f;

    [Header("Needed resources")]
    public Collider magnetArea;
    public Healthbar healthBar;
    public ExpirenceBar expirenceBar;
    public Transform moduleGridContainer;
    public Renderer playerRenderer;

    [Header("Values for visual")]
    public float tilt = 4f;

    private Camera cam;
    private Rigidbody rb;
    private Coroutine lightCoroutine;
    private Vector3 baseMagnetScale;
    private Queue<int> moduleChoicesQueue = new Queue<int>();
    private bool isChoosingModule;
    private float invincibilityCounter;
    private float flashCounter;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else 
            Destroy(gameObject);
    }

    //private void OnEnable()
    //{
    //    ItemInventory.instance.OnInventoryChanged += RecalculateStats;
    //}

    //private void OnDisable()
    //{
    //    ItemInventory.instance.OnInventoryChanged -= RecalculateStats;
    //}

    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();

        baseSpeed = speed;
        baseFireRate = fireRate;
        baseMagnetBonus = magnetBonus;

        if (magnetArea != null)
        {
            baseMagnetScale = magnetArea.transform.localScale;

            magnetArea.transform.position = transform.position;
            magnetArea.transform.SetParent(null);
        }

        UpdateMagnetArea();

        playerCurrentHealth = playerMaxHealth;
        healthBar.SetMaxHealth(playerMaxHealth);

        expirenceBar.SetMaxExp(maxExp);
        expirenceBar.SetExp(currentExp);

        level = 1;
        R.instance.levelText.text = "lv." + level.ToString();
    }

    public void Update()
    {
        if ((Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space)) && Time.time > nextFire) {
            R.instance.shotEffect.Play();
            StartMuzzleFlash();
            nextFire = Time.time + fireRate;
            Instantiate(R.instance.bolt, R.instance.playerShotSpawn.position, R.instance.playerShotSpawn.rotation);
        }

        if (Input.GetKeyDown(KeyCode.L))
            GainExp(maxExp);

        if (Input.GetKeyDown(KeyCode.K))
            GameController.instance.AddScore(250);

        if(invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if(flashCounter <= 0)
            {
                playerRenderer.enabled = !playerRenderer.enabled;
                flashCounter = flashLength;
            }

            if(invincibilityCounter <= 0)
            {
                playerRenderer.enabled = true;
            }
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
        if (GetComponent<PlayerShield>().IsActive())
        {
            GameController.instance.AddScore(10);
            return;
        }

        if(invincibilityCounter <= 0)
        {

            var laserHitEffect = Instantiate(R.instance.lazerRayHit, transform.position, Quaternion.identity);
            laserHitEffect.transform.SetParent(transform, true);

            var hitEffect = Instantiate(R.instance.hitEffect, transform.position, Quaternion.identity);
            hitEffect.transform.SetParent(transform, true);

            playerCurrentHealth -= value;
            healthBar.SetHealth(playerCurrentHealth);

            invincibilityCounter = invincibilityLength;

            playerRenderer.enabled = false;

            flashCounter = flashLength;

            if (playerCurrentHealth <= 0)
            {
                Die();
                return;
            }

        }
    }

    private void Die()
    {
        if (gameObject != null)
        {
            Destroy(gameObject);
            Instantiate(R.instance.playerExplosionEffect, transform.position, Quaternion.identity);
            Time.timeScale = 0f;
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
            while (currentExp >= maxExp)
            {
                currentExp -= maxExp;
                maxExp *= 1.25f;
                expirenceBar.SetMaxExp(maxExp);
                level++;
                R.instance.levelText.text = "lv. " + level;

                moduleChoicesQueue.Enqueue(1); // добавление запроса на выбор модуля в очередь
                expirenceBar.SetExp(currentExp);
                expirenceBar.StartRainbow();

            }
            
            if(!isChoosingModule)
            {
                StartCoroutine(ProcessModuleChoiceQueue());
            }

            SpawnLevelUpText();
        }

        expirenceBar.SetExp(currentExp);
    }

    private IEnumerator ProcessModuleChoiceQueue()
    {
        isChoosingModule = true;
        R.instance.moduleSelectionPanel.SetActive(true);

        while(moduleChoicesQueue.Count > 0)
        {
            moduleChoicesQueue.Dequeue(); // удалить обработанный случай из очереди

            ShowModuleChoices();
            magnetArea.GetComponent<Magnet>().gameObject.SetActive(false);
            Time.timeScale = 0;

            while(Time.timeScale == 0) yield return null;

            yield return null;
        }

        isChoosingModule = false;
        R.instance.moduleSelectionPanel.SetActive(false);
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

    private void StartMuzzleFlash()
    {
        if (lightCoroutine != null) StopCoroutine(lightCoroutine);
        R.instance.muzzleLight.gameObject.SetActive(true);
        R.instance.muzzleLight.intensity = 45f;
        lightCoroutine = StartCoroutine(FadeLight());
    }

    private IEnumerator FadeLight()
    {
        yield return new WaitForSeconds(0.1f * 0.3f);
        float fadeTime = 0.1f * 0.7f;
        float startIntensity = R.instance.muzzleLight.intensity;
        float timer = 0;

        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            R.instance.muzzleLight.intensity = Mathf.Lerp(startIntensity, 0f, timer / fadeTime);
            yield return null;
        }
        R.instance.muzzleLight.gameObject.SetActive(false);
    }

    public void UpdateMagnetArea()
    {
        magnetArea.transform.localScale = new Vector3(
            baseMagnetScale.x + magnetBonus + pickupMagnetBonus,
            baseMagnetScale.y + magnetBonus + pickupMagnetBonus,
            baseMagnetScale.z + magnetBonus + pickupMagnetBonus
        );
    }

    public void RecalculateStats()
    {

    }
}
