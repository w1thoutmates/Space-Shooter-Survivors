using UnityEngine;

public class ReachZone : MonoBehaviour
{
    public float timeToReach = 5f;
    public GameObject reachZoneObj;
    public GameObject signalPS;

    private float reachTimer = 0f;
    private bool isReaching = false;

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

        if (reachTimer >= timeToReach)
        {
            Debug.Log("You are reached the zone!");

            // зона захвачена
            // бафф

            PlayerController.instance.GainExp(PlayerController.instance.maxExp);
            reachTimer = 0;

            var s = GetComponentInParent<Supply>();

            Destroy(s.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isReaching = true;
            reachZoneObj.SetActive(true);
            signalPS.GetComponent<ParticleSystem>().Stop();
            signalPS.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isReaching = false;
            reachZoneObj.SetActive(false);
            signalPS.GetComponent<ParticleSystem>().Play();
            signalPS.SetActive(true);
        }
    }
}
