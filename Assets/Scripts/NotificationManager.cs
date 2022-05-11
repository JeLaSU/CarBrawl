using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public GameObject notifyCheckpoint;
    public GameObject notifySecondLap;
    public GameObject notifyLastLap;

    float timeToLast1 = 0.5f, timeToLast2 = 1f, timeToLast3 = 1f;

    bool checkpointTimerStart = false, secondLapTimerStart = false, 
        lastLapTimerStart = false, repeatLastLap = true, repeatSecondLap = true;

    public void Awake()
    {
        notifyCheckpoint.GetComponent<GameObject>();
        notifySecondLap.GetComponent<GameObject>();
        notifyLastLap.GetComponent<GameObject>();
    }
    public void Update()
    {
        if (checkpointTimerStart)
        {
            timeToLast1 -= Time.deltaTime;

            if (timeToLast1 <= 0.0f)
            {
                CheckpointTimerEnded();
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

    public void NotifyCheckpoint(bool status)
    {
        
        notifyCheckpoint.gameObject.SetActive(status);
        checkpointTimerStart = status;
    }
    public void NotifySecondLap(bool status)
    {
        if (repeatSecondLap)
        {
            notifySecondLap.gameObject.SetActive(status);
            secondLapTimerStart = status;
        }
    }
    public void NotifyLastLap(bool status)
    {
        if (repeatLastLap)
        {
            notifyLastLap.gameObject.SetActive(status);
            lastLapTimerStart = status;
        }
    }


    void CheckpointTimerEnded()
    {
        notifyCheckpoint.gameObject.SetActive(false);
        timeToLast1 = 0.5f;
        checkpointTimerStart = false;
    }
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
