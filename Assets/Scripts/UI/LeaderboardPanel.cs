using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardPanel : MonoBehaviour
{
    public List<Text> placesTexts;

    void Start()
    {
        // ... 
    }

    void LateUpdate()
    {
        List<string> places = LeaderboardLogic.GetPlaces();
        for(int i=0; i < placesTexts.Count; i++)
        {
            if(i < places.Count)
            {
                placesTexts[i].text = places[i];
            }
            else
            {
                placesTexts[i].text = "";
            }
        }
    }
}
