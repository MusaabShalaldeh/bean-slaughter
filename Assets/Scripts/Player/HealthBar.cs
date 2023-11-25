using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("References")]
    public Image Filler;
    public TMP_Text PercentageText;

    // Private Variables
    float maxHealth;

    public void Initilize(float _maxHealth)
    {
        maxHealth = _maxHealth;
        UpdateHealthBar(maxHealth);
    }

    public void UpdateHealthBar(float amount)
    {
        amount = Mathf.Clamp(amount, 0, maxHealth);
        float percentage = amount / maxHealth;

        Filler.fillAmount = percentage;
        PercentageText.text = Mathf.Ceil(percentage * 100).ToString() + "%";
    }
}
