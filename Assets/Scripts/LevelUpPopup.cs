using UnityEngine;
using TMPro;
using System.Collections;

public class LevelUpPopup : MonoBehaviour
{
    public float floatSpeed = 1.5f;
    public float lifeTime = 2f;
    public float rotationSpeed = 60f;

    private float timer;
    private TextMeshProUGUI textMesh;
    private Color startColor;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        startColor = textMesh.color;
    }

    private void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        timer += Time.deltaTime;
        float alpha = Mathf.Lerp(startColor.a, 0f, timer / lifeTime);
        textMesh.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

        if (timer >= lifeTime)
            Destroy(gameObject);
    }

}
