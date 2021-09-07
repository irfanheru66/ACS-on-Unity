using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderSystem : MonoBehaviour
{
    [SerializeField] private Slider antSlider;

    [SerializeField] private TextMeshProUGUI antText;

    public void AntSlider(float value)
    {
        antText.text = (Mathf.RoundToInt(value)).ToString();
    }
}
