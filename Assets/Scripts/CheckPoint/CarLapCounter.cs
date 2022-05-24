using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CarLapCounter : MonoBehaviour
{
    //c# event
    public event Action<CarLapCounter> onPassCheckpoint;

    public static CarLapCounter instance;

    //Text objekt matas in ifrån unity.
    public Text carLapText;
    public Text counterTex;

    private int passedCheckPointNumber = 0, numberOfPassedCheckpoints = 0, 
        carPosition = 0, lapsCompleted = 1, lapsToComplete = 4;

    private float timeAtLastCheckpoint = 0;

    public bool stopTimer = false;

    

    public void Awake()
    {
        instance = this;
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
        
        if (collider2D.CompareTag("CheckPoint"))
        {
            CheckPoint checkPoint = collider2D.GetComponent<CheckPoint>();

            //status of checkpoints driven in the correct order. The next checkpoint must have 1 higher value than the passed one
            if (passedCheckPointNumber + 1 == checkPoint.checkPointNumber)
            {
                passedCheckPointNumber = checkPoint.checkPointNumber;

                numberOfPassedCheckpoints++;

                //Catches the time from the last checkpoint.
                timeAtLastCheckpoint = Time.time;

                //If all checkpoints are driven through.
                if (checkPoint.isFinishLine)
                {
                    passedCheckPointNumber = 0;
                    lapsCompleted++;

                    if (lapsCompleted == lapsToComplete)
                    {
                        CountDownManager.instance.OpenCountDisplay();
                    }
                    
                }

                //Invoke the checkpoint which the car passed
                onPassCheckpoint?.Invoke(this);
            }
        }

    }
    private void Update()
    {
        LapUpdate();
    }
    private void LapUpdate()
    {
        if (CompareTag("Player") && lapsCompleted < lapsToComplete)
        {
            //Change the label of laps, based by the lapscompleted. +1, because we want it to start as 1/3.
            carLapText.GetComponent<Text>().text = lapsCompleted + " / 3";

        }
    }
}
