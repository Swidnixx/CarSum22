using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public Vector3[] positions;
    public CinemachineVirtualCamera cam;
    int activePosition = 0;

    private void Start()
    {
        if (positions.Length == 0) return;

        cam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = positions[activePosition];
    }

    private void Update()
    {
        if (positions.Length == 0) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            activePosition++;
            activePosition = activePosition % positions.Length;
            cam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = positions[activePosition];
        }
    }

    public void SetCameraProperties(GameObject car)
    {
        cam.Follow = car.GetComponent<DrivingScript>().rb.transform;
        cam.LookAt = car.GetComponent<DrivingScript>().rb.transform;
    }
}
