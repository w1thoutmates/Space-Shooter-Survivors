using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("drone")) return;
        Destroy(other.gameObject);
    }
}
