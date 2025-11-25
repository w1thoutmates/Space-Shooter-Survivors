
using System.Collections;
using UnityEngine;

public class SuppliesSystem : MonoBehaviour
{
    public static SuppliesSystem instance;

    [Header("Spawn Values")]
    public float spawnX = 10f;
    public float spawnZ = 2.5f;
    public float spawnY = 5.6f;

    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void SpawnSupply()
    {
        GameObject obj = Instantiate(R.instance.supply, GetSpawnPosition(), Quaternion.identity);

        Supply s = obj.GetComponent<Supply>();

        float x = obj.transform.position.x;

        switch (x)
        {
            case 10:
                s.visual.transform.rotation = Quaternion.Euler(8f, 50f, 27f);
                s.reachZone.transform.rotation = Quaternion.Euler(70f, 25f, 0f);
                s.reachZone.transform.localPosition = new Vector3(0.4f, 0, 0.61f);
                break;

            case -10:
                s.visual.transform.rotation = Quaternion.Euler(8f, -38f, -27f);
                s.reachZone.transform.rotation = Quaternion.Euler(70f, -20f, 0f);
                s.reachZone.transform.localPosition = new Vector3(-0.4f, 0, 0.61f);
                break;
        }
    }

    private Vector3 GetSpawnPosition()
    {
        float x = Random.Range(0, 2) == 0 ? spawnX : -spawnX;
        return new Vector3(x, spawnY, spawnZ);
    }

    public IEnumerator SpawnSuppliesCoroutine(float spawnTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);

            SpawnSupply();
        }
    }
}