using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float magnetForce = 4000f;
    public float maxSpeed = 20f;
    public float pickupDistance = 1f;

    private readonly List<Rigidbody> objects = new List<Rigidbody>();

    private void FixedUpdate()
    {
        for (int i = objects.Count - 1; i >= 0; i--)
        {
            Rigidbody rb = objects[i];
            if (rb == null) { objects.RemoveAt(i); continue; }

            Vector3 dir = (transform.position - rb.position).normalized;
            Vector3 targetVel = dir * maxSpeed;
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, targetVel, magnetForce * Time.fixedDeltaTime);

            if (Vector3.Distance(transform.position, rb.position) < pickupDistance)
            {
                if (rb.CompareTag("expirence"))
                {
                    Expirence exp = rb.GetComponent<Expirence>();
                    if (exp != null)
                    {
                        exp.Collect(PlayerController.instance);
                    }
                }
                objects.RemoveAt(i);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("expirence"))
        {
            Rigidbody rb = other.attachedRigidbody;
            if (rb != null && !objects.Contains(rb))
                objects.Add(rb);
        }
    }

}