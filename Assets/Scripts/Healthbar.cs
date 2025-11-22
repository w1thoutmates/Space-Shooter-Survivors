using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private float maxHealth;
    private float currentHealth;

    public void SetMaxHealth(float value)
    {
        maxHealth = value;
        currentHealth = value;
        UpdateHB();
    }

    public void SetHealth(float value)
    {
        currentHealth = Mathf.Clamp(value, 0, maxHealth);
        UpdateHB();
    }

    private void UpdateHB()
    {
        if (R.instance.hbFillImage != null && maxHealth > 0)
        {
            R.instance.hbFillImage.fillAmount = currentHealth / maxHealth;
            UpdateText();
        }
    }

    private void UpdateText()
    {
        if (R.instance.healthText == null) return;

        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
        {
            R.instance.healthText.text = $"{currentHealth:F1}/{maxHealth:F1}";
        }
        else
        {
            R.instance.healthText.text = $"{Mathf.RoundToInt(currentHealth)}/{Mathf.RoundToInt(maxHealth)}";
        }
    }

    private void LateUpdate()
    {
        if (R.instance.healthText != null)
        {
            UpdateText();
        }
    }
}
