using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    DrivingScript drivingScript;

    CheckpointController checkpointController;
    float lastTimeMoving = 0;
    public float roadLevel = 15.5f;

    void Start()
    {
        drivingScript = GetComponent<DrivingScript>();
        checkpointController = drivingScript.rb.GetComponent<CheckpointController>();
    }

    void Update()
    {
        if (checkpointController.lap == RaceController.totalLaps + 1) return;

        float accel = Input.GetAxis("Vertical");
        float steer = Input.GetAxis("Horizontal");
        float brake = Input.GetAxis("Jump");

        if(drivingScript.rb.velocity.magnitude > 1 || !RaceController.racePending)
        {
            lastTimeMoving = Time.time;
        }

        if(Time.time > lastTimeMoving + 4 || drivingScript.rb.transform.position.y < roadLevel - 5)
        {
            //postawienie samochodu z powrotem na torze
            drivingScript.rb.velocity = Vector3.zero;
            drivingScript.rb.transform.position = checkpointController.lastCheckpoint.transform.position; // zwróciæ uwagê na rozmieszczenie checkpointów
            drivingScript.rb.transform.rotation = checkpointController.lastCheckpoint.transform.rotation;
        }

        if (!RaceController.racePending)
            accel = 0;

        drivingScript.Drive(accel, brake, steer);
    }

}
