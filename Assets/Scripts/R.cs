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
    public GameObject medKit;

    [Header("VFX")]
    public GameObject explosionAsteroid;
    public GameObject lazerRayHit;
    public GameObject pickupEffect;

    [Header("UI")]
    public Image fillImage;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;


    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

}
