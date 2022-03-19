using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public int lap = 0;
    public int checkpoint = -1;
    public int checkpointsCount;
    public int nextCheckpoint = 0;

    void Start()
    {
        checkpointsCount = GameObject.FindGameObjectsWithTag("Checkpoint").Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Checkpoint"))
        {
            int collidedCheckpoint = int.Parse(other.name);
            if(collidedCheckpoint == nextCheckpoint)
            {
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
