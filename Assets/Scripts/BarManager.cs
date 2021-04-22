using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BarManager : MonoBehaviour
{
    public Slider slider;
    public TMP_Text text;
    public void SetValue(int value, int delta)
    {
        slider.value = value;
        text.text = value.ToString() + "\n(+" + delta.ToString() + ")";
    }
    public void SetMaxValue(int max)
    {
        slider.maxValue = max;
    }
    public void SetTextColor(Color color)
    {
        text.color = color;
    }
}
