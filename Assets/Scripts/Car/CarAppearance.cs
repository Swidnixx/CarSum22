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

    public Camera backCamera;

    int carID;
    bool idSet = false;
    public CheckpointController checkpointController;

    private void LateUpdate()
    {
        if(!idSet)
        {
            carID = LeaderboardLogic.Register(playerName);
            idSet = true;
            return;
        }
        LeaderboardLogic.SetPosition(carID, checkpointController.lap, checkpointController.checkpoint);
    }

    public void SetNameAndColor(string name, Color color)
    {
        playerName = name;
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
        backCamera.targetTexture = rt;
        FindObjectOfType<RaceController>().SetMirror(backCamera);
    }
}
