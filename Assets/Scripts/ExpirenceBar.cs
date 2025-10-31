using UnityEngine;

public class ExpirenceBar : MonoBehaviour
{
    private float maxExp;
    private float currentExp;

    public void SetMaxExp(float value)
    {
        maxExp = Mathf.Max(value, 1f);
        UpdateEB();
    }

    public void SetExp(float value)
    {
        currentExp = Mathf.Clamp(value, 0, maxExp);
        UpdateEB();
    }

    void UpdateEB()
    {
        if (R.instance.ebFillImage != null)
            R.instance.ebFillImage.fillAmount = currentExp / maxExp;
    }
}
