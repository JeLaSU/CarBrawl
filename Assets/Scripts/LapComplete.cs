using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapComplete : MonoBehaviour
{
    

    public GameObject lapCompleteTrig, lapStartTrig, 
        minuteDisplay, secondDisplay, milliDisplay, lapTimeBox;
    
    void OnTriggerEnter()
    {
        if(TimerManager.secondCount <= 9)
        {
            secondDisplay.GetComponent<Text>().text = "0" + TimerManager.secondCount + ".";
        }
        else
        {
            secondDisplay.GetComponent<Text>().text = "" + TimerManager.secondCount + ".";
        }

        if (TimerManager.minuteCount <= 9)
        {
            minuteDisplay.GetComponent<Text>().text = "0" + TimerManager.minuteCount + ":";
        }
        else
        {
            minuteDisplay.GetComponent<Text>().text = "" + TimerManager.minuteCount + ":";
        }

        milliDisplay.GetComponent<Text>().text = "" + TimerManager.milliCount;

        TimerManager.milliCount = 0;
        TimerManager.secondCount = 0;
        TimerManager.minuteCount = 0;

        lapCompleteTrig.SetActive(true);
        lapStartTrig.SetActive(false);
        Debug.Log("You crossed the line");
    }

}
