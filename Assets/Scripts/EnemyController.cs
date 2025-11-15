using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : Enemy
{
    [Header("Maneuver values")]
    public float xBoundary = 10f; 
    public float xPadding = 2f;
    public float maneuverSmoothTime = 0.5f; // чем меньше, тем резче.

    public TrailRenderer tr;

    private float targetZ;
    private float newX = 0f;

    private bool isManeuvering = false;
    private float currentTargetX;

    private float _zStopVelocity = 0f;


    protected override void Start()
    {
        base.Start();

        targetZ = Random.Range(6f, 10f);
    }


    private void FixedUpdate()
    {
        if (!isManeuvering)
        {
            const float brakingDistance = 0.75f;

            if (Mathf.Abs(transform.position.z - targetZ) > brakingDistance)
            {
                rb.linearVelocity = transform.forward * -enemySpeed;
            }
            else
            {
                float newZ = Mathf.SmoothDamp(
                    transform.position.z,
                    targetZ,
                    ref _zStopVelocity,
                    0.1f
                );

                rb.MovePosition(new Vector3(transform.position.x, transform.position.y, newZ));

                if (Mathf.Abs(transform.position.z - targetZ) < 0.05f)
                {
                    rb.linearVelocity = Vector3.zero;
                    isManeuvering = true;
                    PickNewManeuverTarget();

                    if (tr != null)
                    {
                        tr.emitting = true;
                    }
                }
            }
        }
        else
        {
            newX = Mathf.Lerp(transform.position.x, currentTargetX, Time.fixedDeltaTime / maneuverSmoothTime);
            rb.MovePosition(new Vector3(newX, transform.position.y, transform.position.z));

            if (Mathf.Abs(transform.position.x - currentTargetX) < 0.1f)
            {
                StartCoroutine(DashEffect());

                PickNewManeuverTarget();
            }
        }

        float currentTiltMultiplier = 1f;
        float tilt = (currentTargetX - transform.position.x) * currentTiltMultiplier;
        transform.rotation = Quaternion.Euler(0, 0, -tilt);
    }

    private void PickNewManeuverTarget()
    {
        xPadding = Random.Range(2f, 4f);

        if (transform.position.x < 0)
        {
            currentTargetX = Random.Range(xBoundary - xPadding, xBoundary);
        }
        else
        {
            currentTargetX = Random.Range(-xBoundary, -xBoundary + xPadding);
        }

        StartCoroutine(DashEffect());
    }

    private IEnumerator DashEffect()
    {
        if (tr == null) yield break;

        tr.emitting = true;

        yield return new WaitForSeconds(1.4f);

        tr.emitting = false;
    }
}
