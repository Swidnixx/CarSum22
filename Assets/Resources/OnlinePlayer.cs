using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OnlinePlayer : MonoBehaviourPunCallbacks
{
    public static GameObject LocalPlayerInstance;

    private void Awake()
    {
        if(photonView.IsMine)
        {
            LocalPlayerInstance = gameObject;
        }
        else
        {
            string playerName = null;
            Color playerColor = Color.white;

            if(photonView.InstantiationData != null)
            {
                playerName = (string)photonView.InstantiationData[0];
                playerColor = new Color((float)photonView.InstantiationData[1],
                    (float)photonView.InstantiationData[2], (float)photonView.InstantiationData[3]);
            }

            if(playerName != null)
            {
                GetComponent<CarAppearance>().SetNameAndColor(playerName, playerColor);
            }
        }
    }
}
