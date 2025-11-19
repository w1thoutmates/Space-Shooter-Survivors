using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum DroneType { Attack, Defense }

public class DroneController : MonoBehaviour
{
    [Header("Settings")]
    public float radius = 3f;
    public float rotationSpeed = 5f; 
    public float smoothTime = 60f;

    [System.Serializable]
    public class DroneData
    {
        public Transform transform; 
        public DroneType droneType;  
    }

    private List<DroneData> spawnedDrones = new List<DroneData>();
    private float globalAngle = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            ItemInventory.instance.Add(R.instance.items[1]);

        if (Input.GetKeyDown(KeyCode.O))
            ItemInventory.instance.Add(R.instance.items[2]);
    }

    void LateUpdate()
    {
        if (spawnedDrones.Count == 0) return;

        globalAngle += rotationSpeed * Time.deltaTime;
        if (globalAngle >= 360f) globalAngle -= 360f;

        UpdateDronePositions();
    }

    public void AddDrone(GameObject dronePrefab, DroneType droneType)
    {
        GameObject newDrone = Instantiate(dronePrefab, transform.position, Quaternion.Euler(0f, 90f, 0f));

        DroneData data = new DroneData();
        data.transform = newDrone.transform;
        data.droneType = droneType;
        data.transform.SetParent(gameObject.transform);

        spawnedDrones.Add(data);
    }

    void UpdateDronePositions()
    {
        int count = spawnedDrones.Count;
        float angleStep = 360f / count;

        for (int i = 0; i < count; i++)
        {
            Transform droneT = spawnedDrones[i].transform;

            float currentDroneAngle = globalAngle + (angleStep * i);

            float angleRad = currentDroneAngle * Mathf.Deg2Rad;

            Vector3 offset = new Vector3(Mathf.Cos(angleRad), 0, Mathf.Sin(angleRad)) * radius;
            Vector3 targetPosition = transform.position + offset;

            float dist = Vector3.Distance(droneT.position, targetPosition);
            float t = Mathf.Clamp01(dist / radius);
            float dynamicSmooth = Mathf.Lerp(60f, 10f, t);

            droneT.position = Vector3.Lerp(droneT.position, targetPosition, Time.deltaTime * dynamicSmooth);
            droneT.rotation = Quaternion.Lerp(droneT.rotation, Quaternion.Euler(0f, 90f, 0f), Time.deltaTime * dynamicSmooth);
        }
    }

    public void RemoveLastDrone(DroneType droneType)
    {
        for (int i = spawnedDrones.Count - 1; i >= 0; i--)
        {
            if (spawnedDrones[i].droneType == droneType)
            {
                if (spawnedDrones[i].transform != null)
                {
                    Destroy(spawnedDrones[i].transform.gameObject);
                }
                spawnedDrones.RemoveAt(i);

                return;
            }
        }
    }

}
