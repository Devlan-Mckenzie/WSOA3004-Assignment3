using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxStamina(int Stamina)
    {
        slider.maxValue = Stamina;
        slider.value = Stamina;
    }

    public void SetStamina(int stamina)
    {
        slider.value = stamina;
    }
}
