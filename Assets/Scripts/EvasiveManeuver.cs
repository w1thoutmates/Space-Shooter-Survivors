using NUnit.Framework.Constraints;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class EvasiveManeuver : MonoBehaviour
{
    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public float dodge;
    public float maneuverSpeed;

    private float targetManeuver;
    private float currentSpeed;
    private Rigidbody rb;
    private float _tilt;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _tilt = GetComponent<UFOEnemyController>().tilt;
        currentSpeed = rb.linearVelocity.z;
        StartCoroutine(Evade());
    }

    private IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x,startWait.y));

        while ( true )
        {
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);

            yield return new WaitForSeconds(Random.Range(maneuverTime.x,maneuverTime.y));

            targetManeuver = 0;

            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }

    private void FixedUpdate()
    {
        float newManeuver = Mathf.MoveTowards(rb.linearVelocity.x, targetManeuver, maneuverSpeed * Time.fixedDeltaTime);

        rb.linearVelocity = new Vector3(newManeuver, 0.0f, currentSpeed);

        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, R.instance.xMin, R.instance.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, R.instance.zMin, R.instance.zMax)
        );

        rb.rotation = Quaternion.Euler(0, 0, rb.linearVelocity.x * -(_tilt));
    }

}
