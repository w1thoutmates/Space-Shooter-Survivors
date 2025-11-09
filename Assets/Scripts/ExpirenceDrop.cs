using UnityEngine;

public class ExpirenceDrop : MonoBehaviour
{
    public enum ExpirenceType { SimpleExpirence, ModifiedExpirence}

    public ExpirenceType type; 

    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;

        switch (type)
        {
            case ExpirenceType.SimpleExpirence:
                Instantiate(R.instance.simpleExpirence, transform.position, Quaternion.identity);
                break;
            case ExpirenceType.ModifiedExpirence:
                Instantiate(R.instance.modifiedExpirence, transform.position, Quaternion.identity);
                break;
        }
    }
}