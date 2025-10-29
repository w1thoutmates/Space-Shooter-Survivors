using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float tilt = 4f;
    public float distanceFromCam = 10f;

    public GameObject bolt;
    public Transform shotSpawn;
    public float fireRate = 0.5f;
    public float nextFire = 0.0f;

    private Camera cam;
    private Rigidbody rb;
    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        if(Input.GetButton("Fire1") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Instantiate(bolt, shotSpawn.transform.position, shotSpawn.rotation);
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rb.rotation = Quaternion.Euler(
            0f,
            180f,
            rb.linearVelocity.x * tilt); 

        Vector3 move = new Vector3(moveHorizontal, 0f, moveVertical) * speed;
        rb.linearVelocity = move;

        Vector3 viewPos = cam.WorldToViewportPoint(rb.position);

        viewPos.x = Mathf.Clamp(viewPos.x, 0.05f, 0.95f);
        viewPos.y = Mathf.Clamp(viewPos.y, 0.05f, 0.95f);

        float dynamicDistance = distanceFromCam + rb.position.z * 0.5f;
        Vector3 targetPos = cam.ViewportToWorldPoint(new Vector3(viewPos.x, viewPos.y, dynamicDistance));

        rb.position = Vector3.Lerp(rb.position, targetPos, speed * Time.fixedDeltaTime);
    }

}
