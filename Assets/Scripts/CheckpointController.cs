using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public int lap = 0;
    public int checkpoint = -1;
    public int checkpointsCount;
    public int nextCheckpoint = 0;

    public GameObject lastCheckpoint;

    void Start()
    {
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        checkpointsCount = checkpoints.Length;
        for(int i=0; i<checkpointsCount; i++)
        {
            if(checkpoints[i].name == "0")
            {
                lastCheckpoint = checkpoints[i];
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Checkpoint"))
        {
            int collidedCheckpoint = int.Parse(other.name);
            if(collidedCheckpoint == nextCheckpoint)
            {
                lastCheckpoint = other.gameObject;
                checkpoint = collidedCheckpoint;
                if(checkpoint == 0)
                {
                    lap++;
                    Debug.Log("Lap: " + lap);
                }
                nextCheckpoint++;
                nextCheckpoint = nextCheckpoint % checkpointsCount;
            }
        }
    }
}
