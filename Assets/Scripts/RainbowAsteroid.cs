using UnityEngine;

public class RainbowAsteroid : MonoBehaviour
{
    private Renderer asteroidRenderer;
    public float speed = 1f; 

    private void Start()
    {
        if (asteroidRenderer == null)
            asteroidRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (asteroidRenderer != null)
        {
            float hue = Mathf.PingPong(Time.time * speed, 1f);
            Color color = Color.HSVToRGB(hue, 1f, 1f);
            asteroidRenderer.material.color = color;
        }
    }
}
