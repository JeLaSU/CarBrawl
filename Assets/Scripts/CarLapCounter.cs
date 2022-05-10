using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CarLapCounter : MonoBehaviour
{
    int passedCheckPointNumber = 0;

    int numberOfPassedCheckpoints = 0;
    public int lapsCompleted;

    public int lapsToComplete = 3;

    public Text carLapText;

    public bool wrongCheckpoint = false;

    //c# event
    public event Action<CarLapCounter> onPassCheckpoint;
    public event Action<CarLapCounter> fakePassCheckPoint;



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


                if (checkPoint.isFinishLine) //If all checkpoints are driven through.
                {
                    passedCheckPointNumber = 0;
                    lapsCompleted++;
                    

                    //if(lapsCompleted >= lapsToComplete) //Will use for the future
                    //{
                    //}
                }

                //Invoke the checkpoint which the car passed
                onPassCheckpoint?.Invoke(this);
                
            }
        }
    }
    private void Update()
    {
        carLapText.GetComponent<Text>().text = lapsCompleted + 1  + " / 3";
    }
}
