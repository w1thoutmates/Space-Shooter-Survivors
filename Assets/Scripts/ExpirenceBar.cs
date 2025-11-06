using UnityEngine;
using UnityEngine.UI;

public class ExpirenceBar : MonoBehaviour
{
    private float maxExp;
    private float currentExp;
    private float displayedExp; 
    public float smoothSpeed = 5f;

    private Image fillImage;
    private Color originalColor;
    private bool rainbowActive;
    private float hue;

    private float savedDisplayedExp;

    private void Awake()
    {
        fillImage = transform.Find("Fill (eb)").GetComponent<Image>();
        originalColor = fillImage.color;
    }

    public void SetMaxExp(float value)
    {
        maxExp = Mathf.Max(value, 1f);
        UpdateEB(true);
    }

    public void SetExp(float value)
    {
        currentExp = Mathf.Clamp(value, 0, maxExp);
    }

    void Update()
    {
        if (!rainbowActive && displayedExp != currentExp)
        {
            displayedExp = Mathf.Lerp(displayedExp, currentExp, Time.deltaTime * smoothSpeed);
            UpdateEB();
        }

        if(rainbowActive && fillImage != null)
        {
            hue += Time.unscaledDeltaTime * 0.5f;
            if (hue > 1f) hue -= 1f;
            fillImage.color = Color.HSVToRGB(hue, 1f, 1f);
        }
    }

    void UpdateEB(bool instant = false)
    {
        if (fillImage != null)
        {
            //    float value = instant ? currentExp : displayedExp;
            fillImage.fillAmount = displayedExp / maxExp; 
        }
    }

    public void StartRainbow()
    {
        if (rainbowActive) return;
        
        rainbowActive = true;
        savedDisplayedExp = displayedExp;
        displayedExp = maxExp;
        UpdateEB(true);

        hue = 0f;
    }

    public void StopRainbow()
    {
        if(!rainbowActive) return;

        rainbowActive = false;
        if (fillImage != null)
        { 
            fillImage.color = originalColor;
            displayedExp = savedDisplayedExp;
            UpdateEB(true);
        }
    }
}
