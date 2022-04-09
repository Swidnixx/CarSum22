using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarColor : MonoBehaviour
{
    public Renderer carRenderer;

    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;

    public Color color;

    private void Start()
    {
        float r, g, b;
        r = PlayerPrefs.GetFloat("Red");
        g = PlayerPrefs.GetFloat("Green");
        b = PlayerPrefs.GetFloat("Blue");

        redSlider.value = r;
        greenSlider.value = g;
        blueSlider.value = b;
    }

    void SetCarColor(float red, float green, float blue)
    {
        Color color = new Color(red, green, blue);
        carRenderer.material.color = color;

        PlayerPrefs.SetFloat("Red", red);
        PlayerPrefs.SetFloat("Green", green);
        PlayerPrefs.SetFloat("Blue", blue);
    }

    private void Update()
    {
        float r, g, b;
        r = redSlider.value;
        g = greenSlider.value;
        b = blueSlider.value;

        SetCarColor(r, g, b);
    }
}
