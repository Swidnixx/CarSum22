using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarAppearance : MonoBehaviour
{
    public int playerNumber;

    public string playerName;
    public Color carColor;
    public Text nameText;
    public Renderer carRednerer;

    void Start()
    {
        nameText.text = playerName;
        carRednerer.material.color = carColor;
        nameText.color = carColor;
    }
}
