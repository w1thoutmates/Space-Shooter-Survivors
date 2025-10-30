using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.TakeDamage(1);
            Destroy(gameObject);
        }

        if(other.tag == "bolt")
        {
            var hitEffect = Instantiate(R.instance.lazerRayHit, other.transform.position, Quaternion.identity);
            var destroyEffect = Instantiate(R.instance.explosionAsteroid, other.transform.position, Quaternion.identity);

            GameController.instance.AddScore(10); // 10 - points reached for destroing asteroids

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
