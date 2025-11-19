using UnityEngine;

public class DefenseDroneBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Enemy Bolt Variant(Clone)")
        {
            GameObject hitEffect = Instantiate(R.instance.lazerRayHit, other.transform.position, Quaternion.identity);
            GameObject destroyEffect = Instantiate(R.instance.explosionAsteroid, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }
}
