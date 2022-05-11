using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CarLapCounter : MonoBehaviour
{
    int passedCheckPointNumber = 0;
    float timeAtLastPassedCheckPoint = 0;

    int numberOfPassedCheckpoints = 0;
    
    //c# event
    public event Action<CarLapCounter> onPassCheckpoint;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("CheckPoint"))
        {
            CheckPoint checkPoint = collider2D.GetComponent<CheckPoint>();
            //status of checkpoints driven in the correct order. The next checkpoint must have 1 higher value than the passed one
            if(passedCheckPointNumber + 1 == checkPoint.checkPointNumber)
            {
                //increasing the number by one
                passedCheckPointNumber = checkPoint.checkPointNumber;

                numberOfPassedCheckpoints++;

                //Store the time at the checkpoint
                timeAtLastPassedCheckPoint = Time.time;

                //Invoke the checkpoint which the car passed
                onPassCheckpoint?.Invoke(this);
            }
        }
    }
}
