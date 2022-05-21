using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrivingScript : MonoBehaviour
{
    public WheelScript[] wheels;
    public float torque = 200;
    public float maxSteerAngle = 30;
    public float maxBrakeTorque = 500;
    public float maxSpeed = 200;
    public Rigidbody rb;
    public float currentSpeed;

    public GameObject stopLights;

    public float nitroFuel = 3;
    public GameObject nitroLights;

    Text nitroText;

    private void Start()
    {
        nitroText = GameObject.FindGameObjectWithTag("FuelText").GetComponent<Text>();
        SetFuelText();
    }

    public void SetFuelText()
    {
        nitroText.text = nitroFuel.ToString();
    }

    void NitroBoost(float power)
    {
        rb.AddForce(rb.gameObject.transform.forward * power);
    }

    public void Nitro(bool on)
    {
        if(on && nitroFuel > 0)
        {
            NitroBoost(1000000);
            nitroFuel -= 1;
            SetFuelText();
            nitroLights.SetActive(true);
            Debug.Log(nitroFuel + " nitros left");
        }
        else
        {
            nitroLights.SetActive(false);
        }
    }

    public void Drive(float accel, float brake, float steer)
    {
        accel = Mathf.Clamp(accel, -1, 1);
        steer = Mathf.Clamp(steer, -1, 1) * maxSteerAngle;
        brake = Mathf.Clamp(brake, 0, 1) * maxBrakeTorque;

        if(brake != 0)
        {
            stopLights.SetActive(true);
        }
        else
        {
            stopLights.SetActive(false);
        }

        float thurstTorque = 0;

        currentSpeed = rb.velocity.magnitude * 3; 
        
        if(currentSpeed < maxSpeed)
        {
            thurstTorque = accel * torque;
        }

        foreach(WheelScript wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = thurstTorque;

            if (wheel.frontWheel)
            {
                wheel.wheelCollider.steerAngle = steer;
            } else
            {
                wheel.wheelCollider.brakeTorque = brake;
            }

            Quaternion quat;
            Vector3 position;
            wheel.wheelCollider.GetWorldPose(out position, out quat);
            wheel.wheel.transform.position = position;
            wheel.wheel.transform.rotation = quat;
        }

    }
    
}
