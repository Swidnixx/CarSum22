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
        if(playerNumber == 0)
        {
            playerName = PlayerPrefs.GetString("PlayerName");
            float r, g, b;
            r = PlayerPrefs.GetFloat("Red");
            g = PlayerPrefs.GetFloat("Green");
            b = PlayerPrefs.GetFloat("Blue");
            carColor = new Color(r, g, b);
        }
        else
        {
            playerName = "Player" + playerNumber;
            carColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }

        nameText.text = playerName;
        carRednerer.material.color = carColor;
        nameText.color = carColor;
    }
}
