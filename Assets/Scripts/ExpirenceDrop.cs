using UnityEngine;

public class ExpirenceDrop : MonoBehaviour
{
    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;

        Instantiate(R.instance.expirence, transform.position, Quaternion.identity);
    }
}
