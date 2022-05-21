using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RaceController : MonoBehaviourPunCallbacks
{
    public RawImage mirror;

    public GameObject startRaceButton;
    public GameObject waitForOthersText;

    public GameObject carPrefab;
    public Transform[] spawnPos;
    public int playerCount;

    public static bool racePending = false;
    public static int totalLaps = 1;
    public int timer = 3;

    public CheckpointController[] carControllers;

    public Text startText;
    AudioSource audioSource;
    public AudioClip countSound;
    public AudioClip startSound;

    public GameObject endPanel;

    public void SetMirror(Camera backCamera)
    {
        mirror.texture = backCamera.targetTexture;
    }

    #region Unity Callbacks
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        startText.gameObject.SetActive(false);
        endPanel.SetActive(false);

        startRaceButton.SetActive(false);
        waitForOthersText.SetActive(false);

        int randomPositionIndex = UnityEngine.Random.Range(0, spawnPos.Length);
        Vector3 startPos = spawnPos[randomPositionIndex].position;
        Quaternion startRot = spawnPos[randomPositionIndex].rotation;

        GameObject playerCar = null;

        if(PhotonNetwork.IsConnected)
        {
            startPos = spawnPos[PhotonNetwork.CurrentRoom.PlayerCount - 1].position;
            startRot = spawnPos[PhotonNetwork.CurrentRoom.PlayerCount - 1].rotation;


            object[] instanceData = new object[4];
            instanceData[0] = PlayerPrefs.GetString("PlayerName");
            instanceData[1] = PlayerPrefs.GetFloat("Red");
            instanceData[2] = PlayerPrefs.GetFloat("Green");
            instanceData[3] = PlayerPrefs.GetFloat("Blue");

            if(OnlinePlayer.LocalPlayerInstance == null)
            {
                playerCar = PhotonNetwork.Instantiate(carPrefab.name, startPos, startRot, 0, instanceData);
                playerCar.GetComponent<CarAppearance>().SetLocalPlayer();
            }

            if(PhotonNetwork.IsMasterClient)
            {
                startRaceButton.SetActive(true);
            }else
            {
                waitForOthersText.SetActive(true);
            }
        }

        playerCar.GetComponent<PlayerController>().enabled = true;
        playerCar.GetComponent<DrivingScript>().enabled = true;
    }

    void LateUpdate()
    {
        if (racePending)
        {
            int finishers = 0;
            foreach (CheckpointController c in carControllers)
            {
                if (c.lap == totalLaps + 1)
                {
                    finishers++;
                }
            }
            if (finishers == carControllers.Length)
            {
                //Debug.Log("Race Finished");
                endPanel.SetActive(true);
                racePending = false;
            } 
        }
    }
    #endregion

    #region Race Controlling Methods
    private void CountDown()
    {
        if (timer > 0)
        {
            startText.text = ("Race Starts in " + timer + "...");
            audioSource.PlayOneShot(countSound);
            timer--;
        }
        else
        {
            startText.text = ("Start!");
            audioSource.PlayOneShot(startSound);
            racePending = true;
            CancelInvoke(nameof(CountDown));

            Invoke(nameof(HideStartText), 2);
        }
    }

    private void HideStartText()
    {
        startText.gameObject.SetActive(false);
    }
    public void RestartRace()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion

    #region Photon Controlling Methods
    [PunRPC]
    private void StartRace()
    {
        startText.gameObject.SetActive(true);

        InvokeRepeating(nameof(CountDown), 3, 1);
        startRaceButton.SetActive(false);
        waitForOthersText.SetActive(false);

        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
        carControllers = new CheckpointController[cars.Length];
        for (int i = 0; i < cars.Length; i++)
        {
            carControllers[i] = cars[i].GetComponent<CheckpointController>();
        }
    }

    public void BeginGame()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            photonView.RPC(nameof(StartRace), RpcTarget.All, null);
        }
    }
    #endregion
}
