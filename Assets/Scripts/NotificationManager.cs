using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    //choosing the object of affect
    public GameObject notifyCheckpoint;
    public GameObject notifySecondLap;
    public GameObject notifyLastLap;

    //The time that the notification is alive in.
    float timeToLast1 = 0.5f, timeToLast2 = 1f, timeToLast3 = 1f;

    //Bools for occurences.
    bool checkpointTimerStart = false, secondLapTimerStart = false, 
        lastLapTimerStart = false, repeatLastLap = true, repeatSecondLap = true;

    public void Awake()
    {
        //Get the objects.
        notifyCheckpoint.GetComponent<GameObject>();
        notifySecondLap.GetComponent<GameObject>();
        notifyLastLap.GetComponent<GameObject>();
    }
    public void Update()
    {
        //if the bool is true
        if (checkpointTimerStart)
        {
            //Decrements in a real time.
            timeToLast1 -= Time.deltaTime;

            if (timeToLast1 <= 0.0f)
            {
                CheckpointTimerEnded();
                //checkpoint true as long as timetolast1 is not zero.
            }
        }


        if (secondLapTimerStart)
        {
            timeToLast2 -= Time.deltaTime;

            if (timeToLast2 <= 0.0f)
            {
                SecondLapTimerEnded();
            }
        }


        if (lastLapTimerStart)
        {
            timeToLast3 -= Time.deltaTime;

            if (timeToLast3 <= 0.0f)
            {
                LastLapTimerEnded();
            }
        }

    }
    //Returns the values that were scattered in the other codes.
    public void NotifyCheckpoint(bool status)
    {   //Inside the PositionHandler
        notifyCheckpoint.gameObject.SetActive(status);
        checkpointTimerStart = status;
    }
    public void NotifySecondLap(bool status)
    {   //Inside the CarLapCounter
        if (repeatSecondLap)
        {
            notifySecondLap.gameObject.SetActive(status);
            secondLapTimerStart = status;
        }
    }
    public void NotifyLastLap(bool status)
    {   //Inside the CarLapCounter
        if (repeatLastLap)
        {
            notifyLastLap.gameObject.SetActive(status);
            lastLapTimerStart = status;
        }
    }

    //If the timer is done, the notification disappears and returns the time to it's origin
    void CheckpointTimerEnded()
    {
        notifyCheckpoint.gameObject.SetActive(false);
        timeToLast1 = 0.5f;
        checkpointTimerStart = false;
    }
    //repeat is used, so the notification is not repeated.
    void SecondLapTimerEnded()
    {
        notifySecondLap.gameObject.SetActive(false);
        repeatSecondLap = false;
        timeToLast2 = 1;
    }
    void LastLapTimerEnded()
    {
        notifyLastLap.gameObject.SetActive(false);
        repeatLastLap = false;
        timeToLast3 = 1;
    }
}
