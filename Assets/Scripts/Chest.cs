using System.Collections;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public float rotatingSpeed = 40;
    public float translatingSpeed = 1f;
    
    public GameObject chest;
    public Animator anim;

    private Vector3 targetPos;

    public void SetTargetX(float targetX)
    {
        targetPos = new Vector3(targetX, transform.position.y, transform.position.z);
    }

    private void LateUpdate()
    {
        if (chest != null)
            chest.transform.Rotate(0f, rotatingSpeed * Time.deltaTime, 0f, Space.Self);
    }

    private void FixedUpdate()
    {
        if (ChestSpawner.instance.isOnRightSpawned)
        {
            if (transform.position.x < targetPos.x)
            {
                transform.position = Vector3.Lerp(transform.position, targetPos, translatingSpeed * Time.fixedDeltaTime);
            }
        }
        else
        {
            if (transform.position.x > targetPos.x)
            {
                transform.position = Vector3.Lerp(transform.position, targetPos, translatingSpeed * Time.fixedDeltaTime);
            }
        }
    }

    public IEnumerator Open()
    {
        anim.SetBool("isDestroying", true);

        Item newItem = R.instance.items[Random.Range(0, R.instance.items.Length)];
        OpenChest.instance.ShowAward(newItem);

        BoxCollider collider = GetComponent<BoxCollider>();
        collider.enabled = false;
        yield return new WaitForSeconds(1.55f);

        Instantiate(R.instance.destroyingChestParticles, transform.position, Quaternion.identity);

        AudioManager.instance.PlaySFX(R.instance.explosionSound);
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine(Open());
    }
}
