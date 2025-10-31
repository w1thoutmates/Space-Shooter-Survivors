using System.Collections;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    private GameObject activeShield;
    private float remainingTime;
    private float flashingTime;
    private bool isActive;
    private Coroutine routine;

    public void ActivateShield(float duration, float flashTime)
    {
        if (isActive)
        {
            remainingTime += duration;
            return;
        }

        flashingTime = flashTime;
        remainingTime = duration;
        routine = StartCoroutine(ShieldRoutine());
    }

    private void FixedUpdate()
    {
        if(activeShield != null)
        {
            activeShield.transform.position = gameObject.transform.position;
        }
    }

    private IEnumerator ShieldRoutine()
    {
        isActive = true;
        activeShield = Instantiate(R.instance.shield, transform, false);
        activeShield.transform.localPosition = Vector3.zero;

        float flashTimer = 0f;

        while (remainingTime > 0f)
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime < flashingTime)
            {
                flashTimer += Time.deltaTime;
                if (flashTimer >= 0.3f)
                {
                    activeShield.SetActive(!activeShield.activeSelf);
                    flashTimer = 0f;
                }
            }

            yield return null;
        }

        Destroy(activeShield);
        isActive = false;
    }

    public bool IsActive() => isActive;
}
