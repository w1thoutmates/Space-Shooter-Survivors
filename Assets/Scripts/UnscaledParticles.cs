using UnityEngine;

public class UnscaledParticles : MonoBehaviour
{
    private ParticleSystem[] particleSystems;

    void Start()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>(true);
    }

    void Update()
    {
        if (Time.timeScale < 0.01f)
        {
            foreach (ParticleSystem ps in particleSystems)
            {
                ps.Simulate(Time.unscaledDeltaTime, true, false);
            }
        }
    }
}