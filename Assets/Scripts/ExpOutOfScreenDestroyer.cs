using UnityEngine;

public class ExpOutOfScreenDestroyer : MonoBehaviour
{
    public float minZ = -5.35f;
    private void Update()
    {
        foreach (var exp in GameObject.FindGameObjectsWithTag("expirence"))
        {
            if (exp.transform.position.z < minZ)
                Destroy(exp);
        }
    }
}
