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

    public void Open()
    {
        anim.SetBool("isOpening", true);

        ItemInventory.instance.Add(R.instance.items[Random.Range(0, R.instance.items.Length)], 1);
        // items selection
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Open();
    }
}
