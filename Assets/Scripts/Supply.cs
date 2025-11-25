using UnityEngine;

public class Supply : MonoBehaviour
{
    public float rotatingSpeed = 90f;
    public float fallSpeed = 3f;
    private bool rotating = true;

    [Header("Visual")]
    public GameObject visual;
    public GameObject reachZone;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0f, 179f, 0f);
    }

    private void Update()
    {
        RotateToZero();
        FallToZero();
    }

    private void RotateToZero()
    {
        if (rotating)
        {
            transform.Rotate(0f, -rotatingSpeed * Time.deltaTime, 0f);

            if (transform.eulerAngles.y <= 1f || transform.eulerAngles.y >= 180f)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                rotating = false;
            }
        }        
    }

    private void FallToZero()
    {
        if (transform.position.y <= 0f) return;

        transform.position = Vector3.Lerp(
            transform.position,
            new Vector3(transform.position.x, 0f, transform.position.z),
            fallSpeed * Time.deltaTime
        );
    }
}
