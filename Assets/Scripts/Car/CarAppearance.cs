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

    //void Start()
    //{
    //    if(playerNumber == 0)
    //    {
    //        playerName = PlayerPrefs.GetString("PlayerName");
    //        float r, g, b;
    //        r = PlayerPrefs.GetFloat("Red");
    //        g = PlayerPrefs.GetFloat("Green");
    //        b = PlayerPrefs.GetFloat("Blue");
    //        carColor = new Color(r, g, b);
    //    }
    //    else
    //    {
    //        playerName = "Player" + playerNumber;
    //        carColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    //    }

    //    nameText.text = playerName;
    //    carRednerer.material.color = carColor;
    //    nameText.color = carColor;
    //}

    public void SetNameAndColor(string name, Color color)
    {
        nameText.text = name;
        carRednerer.material.color = color;
        nameText.color = color;
    }

    public void SetLocalPlayer()
    {
        FindObjectOfType<CameraController>().SetCameraProperties(this.gameObject);
        //
        playerName = PlayerPrefs.GetString("PlayerName");
        float r, g, b;
        r = PlayerPrefs.GetFloat("Red");
        g = PlayerPrefs.GetFloat("Green");
        b = PlayerPrefs.GetFloat("Blue");
        carColor = new Color(r, g, b);
        SetNameAndColor(playerName, carColor);

        RenderTexture rt = new RenderTexture(1024, 1024, 0);
        // Tutaj nale¿y uzpe³niæ render texturê lusterka
    }
}
