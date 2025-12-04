using TMPro;
using UnityEngine;
using UnityEngine.UI;


// R - Resources
public class R : MonoBehaviour
{
    public static R instance;

    [Header("Prefabs")]
    public GameObject bolt;
    public GameObject asteroid;
    public GameObject[] pickups;
    public GameObject[] hits;
    public GameObject shield;
    public GameObject simpleExpirence;
    public GameObject modifiedExpirence;
    public GameObject levelUpText;
    public GameObject moduleCard;
    public GameObject enemySpaceShip;
    public GameObject UFOEnemySpaceShip;
    public GameObject hitEffect;
    public GameObject playerModel;
    public GameObject playerExplosionEffect;
    public GameObject chest;
    public GameObject itemImg;
    public Item[] items;
    public GameObject attackDrone;
    public GameObject defenseDrone;
    public GameObject supply;
    public GameObject[] asteroidVariants;

    [Header("VFX")]
    public GameObject explosionAsteroid;
    public GameObject lazerRayHit;
    public GameObject pickupEffect;
    public ParticleSystem shotEffect;
    public GameObject muzzlePoint;
    public Light muzzleLight;
    public GameObject destroyingChestParticles;
    public GameObject lootEffect;

    [Header("UI")]
    public Image hbFillImage;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;
    public Image ebFillImage;
    public TextMeshProUGUI levelText;
    public GameObject moduleSelectionPanel;
    public GameObject itemInventoryUI;
    public HorizontalLayoutGroup itemInventoryLayout;

    [Header("Sounds")]
    public AudioClip[] levelMusics;
    public AudioClip[] laserShotSounds;
    public AudioClip[] enemyLaserShotSounds;
    public AudioClip levelupSound;
    public AudioClip asteroidSmashSound;
    public AudioClip explosionSound;
    public AudioClip[] pickupSounds;
    public AudioClip laserHitSound;
    public AudioClip[] enemyDashSounds;
    public AudioClip chestOpeningSound;
    public AudioClip foundChestSound;
    public AudioClip collectExpSound;

    [Header("Transforms")]
    public Transform playerShotSpawn;

    [Header("Boundary values")]
    public float xMin;
    public float xMax;
    public float zMin;
    public float zMax;

    private int levelMusicIndex = 0;


    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        PlayNextMusic();
    }

    private void LateUpdate()
    {
        if (!AudioManager.instance.musicSource.isPlaying)
        {
            PlayNextMusic();
        }
    }

    private void PlayNextMusic()
    {
        AudioManager.instance.PlayMusic(levelMusics[levelMusicIndex]);

        levelMusicIndex = (levelMusicIndex + 1) % levelMusics.Length;
    }

}
