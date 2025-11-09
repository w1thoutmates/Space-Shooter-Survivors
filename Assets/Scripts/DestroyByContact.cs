using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public float scoreValue = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.TakeDamage(1);
            Destroy(gameObject);
            return;
        }

        if(other.tag == "bolt")
        {
            var enemy = GetComponent<EnemyController>();

            GameObject hitEffect;

            if (enemy != null)
            {
                enemy.TakeDamage(1);
                hitEffect = Instantiate(R.instance.hits[Random.Range(0, R.instance.hits.Length)], enemy.transform.position, Quaternion.identity);
                hitEffect.transform.SetParent(enemy.transform, true);

                if (enemy.IsDead())
                {
                    Instantiate(R.instance.explosionAsteroid, transform.position, Quaternion.identity);
                    GameController.instance.AddScore(scoreValue);
                    Destroy(gameObject);
                }

                Destroy(other.gameObject);
                return;
            }

            hitEffect = Instantiate(R.instance.lazerRayHit, other.transform.position, Quaternion.identity);
            var destroyEffect = Instantiate(R.instance.explosionAsteroid, other.transform.position, Quaternion.identity);
            GameController.instance.AddScore(scoreValue);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
