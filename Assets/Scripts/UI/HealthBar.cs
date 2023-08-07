using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;

    public void SetHealthBarValue(float health, float maxHealth) 
    {
        healthSlider.value = health / maxHealth;
    }
}
