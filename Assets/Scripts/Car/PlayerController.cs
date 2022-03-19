using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    DrivingScript drivingScript;

    void Start()
    {
        drivingScript = GetComponent<DrivingScript>();
    }

    void Update()
    {
        float accel = Input.GetAxis("Vertical");
        float steer = Input.GetAxis("Horizontal");
        float brake = Input.GetAxis("Jump");

        if (!RaceController.racePending)
            accel = 0;

        drivingScript.Drive(accel, brake, steer);
    }

}
