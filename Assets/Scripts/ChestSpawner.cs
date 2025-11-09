using System.Collections;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    public static ChestSpawner instance;

    public float minZ = 0f;
    public float maxZ = 6f;
    public float spawnX = -10f;
    public float xBoundary = 1.5f;
    public float startDelay = 0.5f;
    public float timeBtwSpawns = 1f;

    [HideInInspector] public bool isOnRightSpawned = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    { 
        StartCoroutine(SpawnChests());
    }

    public IEnumerator SpawnChests()
    {
        yield return new WaitForSeconds(startDelay);

        while (true)
        {
            if (Roll(PlayerController.instance.luck))
            {
                float currentSpawnX = isOnRightSpawned ? -spawnX : spawnX;
                float currentTargetX = isOnRightSpawned ? -xBoundary : xBoundary;
                Vector3 spawnPos = new Vector3(currentSpawnX, 0, Random.Range(minZ, maxZ));
                GameObject chest = Instantiate(R.instance.chest, spawnPos, Quaternion.identity);

                chest.GetComponent<Chest>().SetTargetX(currentTargetX);
                isOnRightSpawned = !isOnRightSpawned;
            }

            yield return new WaitForSeconds(timeBtwSpawns);
        }
    }

    private bool Roll(float luck)
    {
        return Random.value < luck;
    }
}
