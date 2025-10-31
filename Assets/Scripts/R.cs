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
    public GameObject shield;
    public GameObject expirence;
    public GameObject levelUpText;
    public GameObject moduleCard;

    [Header("VFX")]
    public GameObject explosionAsteroid;
    public GameObject lazerRayHit;
    public GameObject pickupEffect;

    [Header("UI")]
    public Image hbFillImage;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;
    public Image ebFillImage;
    public TextMeshProUGUI levelText;
    public GameObject moduleSelectionPanel;


    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

}
