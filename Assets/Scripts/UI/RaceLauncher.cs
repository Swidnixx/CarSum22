using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RaceLauncher : MonoBehaviourPunCallbacks
{
    public InputField playerName;

    byte maxPlayerPerRoom = 4;
    bool isConnecting = false;
    public Text networkText;

    #region Photon Callbacks
    public void EstablishConnection()
    {
        if (isConnecting)
            return;

        networkText.text = "";
        isConnecting = true;
        PhotonNetwork.NickName = playerName.text;
        if(PhotonNetwork.IsConnected)
        {
            networkText.text += "Trying to Join a Room...\n";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            networkText.text += "Connecting to a Server...\n";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        if(isConnecting)
        {
            networkText.text += "Successfuly connected to the Server.\n";
            networkText.text += "Trying to Join a Room...\n";
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        networkText.text += "Joining Room Failed:\n";
        networkText.text += returnCode + ": " + message + "\n";

        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = maxPlayerPerRoom });
    }

    public override void OnJoinedRoom()
    {
        networkText.text += "Joined Room with " + PhotonNetwork.CurrentRoom.PlayerCount + " players\n";
        PhotonNetwork.LoadLevel("RaceScene");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        networkText.text += "Disconnected due to: " + cause + "\n";
        isConnecting = false;
    }
    #endregion

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        if(PlayerPrefs.HasKey("PlayerName"))
        {
            playerName.text = PlayerPrefs.GetString("PlayerName");
        }
    }

    public void SetName(string name)
    {
        PlayerPrefs.SetString("PlayerName", name);
    }

    public void LaunchRace()
    {
        SceneManager.LoadScene(1);
    }
}
