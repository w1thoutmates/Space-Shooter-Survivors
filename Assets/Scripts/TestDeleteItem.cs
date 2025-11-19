using UnityEngine;

public class TestDeleteItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ItemInventory.instance.Remove(R.instance.items[1]);
    }
}
