using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderSystem : MonoBehaviour
{
    [SerializeField] public static Slider antSlider;

    [SerializeField] private TextMeshProUGUI antText;

    public static int _numAnt;

    public void AntSlider(float value)
    {
        _numAnt = Mathf.RoundToInt(value);
        antText.text = (_numAnt).ToString();
        
    }
}
