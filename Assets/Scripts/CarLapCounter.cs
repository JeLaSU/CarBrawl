using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CarLapCounter : MonoBehaviour
{
    int passedCheckPointNumber = 0;

    int numberOfPassedCheckpoints = 0;
    float timeAtLastCheckpoint = 0;
    public int lapsCompleted;

    public int lapsToComplete = 3;

    public Text carLapText;

    int carPosition = 0;


    //c# event
    public event Action<CarLapCounter> onPassCheckpoint;
    NotificationManager notificationManager;

    public void Awake()
    {
        //Calling the notificationManager.
        notificationManager = FindObjectOfType<NotificationManager>();
    }

    //Sets the car position from positionHandler. If the car is first or second and so on.
    public void SetCarPosition(int position)
    {
        carPosition = position;
    }
    //It returns on how many checkpoints every car has driven through.
    public int GetNumberOfCheckPointsPassed() 
    {
        return numberOfPassedCheckpoints;
    }
    //It returns the time after the last checkpoint to the newest checkpoint.
    public float GetTimeAtLastCheckPoint() 
    {
        return timeAtLastCheckpoint;
    }

    //If triggering in 2D
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        //If the car is colliding with a obj with tag checkPoint.
        if (collider2D.CompareTag("CheckPoint"))
        {
            CheckPoint checkPoint = collider2D.GetComponent<CheckPoint>();
            //status of checkpoints driven in the correct order. The next checkpoint must have 1 higher value than the passed one
            if(passedCheckPointNumber + 1 == checkPoint.checkPointNumber)
            {
                //increasing the number by one
                passedCheckPointNumber = checkPoint.checkPointNumber;

                numberOfPassedCheckpoints++;

                //Catches the time from the last checkpoint.
                timeAtLastCheckpoint = Time.time;


                //If all checkpoints are driven through.
                if (checkPoint.isFinishLine) 
                {
                    passedCheckPointNumber = 0;
                    lapsCompleted++;
                    

                    //if(lapsCompleted >= lapsToComplete) //Will use for the future
                    //{
                    //}
                }

                //Based on the laps completed, the following notifications will occur.
                if(lapsCompleted == 2)
                {
                    notificationManager.NotifySecondLap(true);
                }
                else if(lapsCompleted == 3)
                {
                    notificationManager.NotifyLastLap(true);
                }

                //Invoke the checkpoint which the car passed
                onPassCheckpoint?.Invoke(this);
                
            }
        }
    }
    private void Update()
    {
        //Change the label of laps, based by the lapscompleted. +1, because we want it to start as 1/3.
        carLapText.GetComponent<Text>().text = lapsCompleted + " / 3";
    }
}
