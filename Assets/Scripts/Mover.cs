using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = rb.transform.forward * speed;
    }
}
