using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ReachZone : MonoBehaviour
{
    public float timeToReach = 5f;
    public GameObject reachZoneObj;
    public GameObject signalPS;
    public Image progressBar;
    public Canvas uiCanvas;

    private float reachTimer = 0f;
    private bool isReaching = false;

    private Image currentBar;
    private RectTransform currentRect;

    private void Start()
    {
        SpawnProgressBar();
    }

    private void Update()
    {
        if(isReaching)
        {
            reachTimer += Time.deltaTime;
            reachTimer = Mathf.Clamp(reachTimer, 0f, timeToReach);
            // Progress bar увеличивается
        }
        else
        {
            reachTimer -= Time.deltaTime;
            reachTimer = Mathf.Clamp(reachTimer, 0f, timeToReach);
            // Progress bar уменьшается
        }

        if (currentBar != null)
        {
            currentBar.fillAmount = reachTimer / timeToReach;

            UpdateProgressBarPosition();

            currentBar.gameObject.SetActive(reachTimer > 0.01f);
        }

        if (reachTimer >= timeToReach)
        {
            Debug.Log("You are reached the zone!");

            // зона захвачена
            // бафф

            PlayerController.instance.GainExp(PlayerController.instance.maxExp);
            reachTimer = 0;

            StartCoroutine(DestroyTheSupply());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isReaching = true;
            reachZoneObj.SetActive(true);
            if (signalPS != null)
            {
                var ps = signalPS.GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                }
                signalPS.SetActive(false);
            }
            progressBar.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isReaching = false;
            reachZoneObj.SetActive(false);
            if (signalPS != null)
            {
                signalPS.SetActive(true);
                var ps = signalPS.GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    ps.Play();
                }
            }
        }
    }

    private IEnumerator DestroyTheSupply()
    {
        yield return new WaitForSeconds(0.1f);
        Instantiate(R.instance.destroyingChestParticles, transform.position, Quaternion.identity);
        AudioManager.instance.PlaySFX(R.instance.explosionSound);

        var s = GetComponentInParent<Supply>();

        Destroy(s.gameObject);
    }

    private void SpawnProgressBar()
    {
        if (progressBar == null || uiCanvas == null) return;

        currentBar = Instantiate(progressBar, uiCanvas.transform);
        currentRect = currentBar.GetComponent<RectTransform>();

        currentBar.fillAmount = 0f;
        currentBar.gameObject.SetActive(false);

        UpdateProgressBarPosition();
    }

    void UpdateProgressBarPosition()
    {
        if (currentRect == null || Camera.main == null) return;

        Vector3 worldPosition = transform.position;
        worldPosition.z += 1.5f;
        worldPosition.y += 1.5f;

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        currentRect.position = screenPosition;

        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(worldPosition);
        bool isVisible = viewportPoint.z > 0 &&
                        viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
                        viewportPoint.y >= 0 && viewportPoint.y <= 1;

        if (currentBar != null)
        {
            currentBar.enabled = isVisible && reachTimer > 0.01f;
        }

    }
}
