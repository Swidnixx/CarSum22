using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RaceController : MonoBehaviour
{
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

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating(nameof(CountDown), 3, 1);

        for(int i=0; i<playerCount; i++)
        {
            GameObject car = Instantiate(carPrefab);
            car.transform.position = spawnPos[i].position;
            car.transform.rotation = spawnPos[i].rotation;
            car.GetComponent<CarAppearance>().playerNumber = i;
            if(i==0)
            {
                car.GetComponent<PlayerController>().enabled = true;
                GameObject.FindObjectOfType<CameraController>().SetCameraProperties(car);
            }
        }

        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
        carControllers = new CheckpointController[cars.Length];
        for(int i=0; i < cars.Length; i++)
        {
            carControllers[i] = cars[i].GetComponent<CheckpointController>();
        }
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
}
