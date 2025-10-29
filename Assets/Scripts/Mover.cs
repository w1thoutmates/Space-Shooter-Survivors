using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed;
    public float distanceFromCam = 10f;

    private Camera cam;
    private Rigidbody rb;

    public void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();

        rb.linearVelocity = rb.transform.forward * speed;

        Vector3 viewPos = cam.WorldToViewportPoint(rb.position);

        viewPos.x = Mathf.Clamp(viewPos.x, 0.05f, 0.95f);
        viewPos.y = Mathf.Clamp(viewPos.y, 0.05f, 0.95f);

        float dynamicDistance = distanceFromCam + rb.position.z * 0.5f;
        Vector3 targetPos = cam.ViewportToWorldPoint(new Vector3(viewPos.x, viewPos.y, dynamicDistance));

        rb.position = Vector3.Lerp(rb.position, targetPos, speed * Time.fixedDeltaTime);

    }
}
