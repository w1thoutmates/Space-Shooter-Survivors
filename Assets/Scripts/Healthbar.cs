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

    void UpdateHB()
    {
        if (R.instance.fillImage != null && maxHealth > 0)
        {
            R.instance.fillImage.fillAmount = currentHealth / maxHealth;
            R.instance.healthText.text = currentHealth.ToString() + "/" + maxHealth.ToString();
        }
    }
}
