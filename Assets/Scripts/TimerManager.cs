using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public static int minuteCount, secondCount;
    public static float milliCount;
    public static string milliDisplay;

    public GameObject minuteBox;
    public GameObject secondBox;
    public GameObject milliBox;

    // Update is called once per frame
    void Update()
    { //VARJE GAMETIME �R MILLIMILLI, G�NGER MAN MED 10, BLIR DEN MILLI.
        milliCount += Time.deltaTime * 10;
        //F0 �r en format av hur tiden �r representerat.
        milliDisplay = milliCount.ToString("F0");
        milliBox.GetComponent<Text>().text = "" + milliDisplay;

        if(milliCount >= 10)
        {
            milliCount = 0;
            secondCount += 1;
        }
        //SKRIVER EN IF SATS, S� ATT EN NOLLA �R FRAMF�R INT NUMRET D�R MAN DISPLAYAR
        if (secondCount <= 9) 
        {
            secondBox.GetComponent<Text>().text = "0" + secondCount + ".";
        }
        else
        {
            secondBox.GetComponent<Text>().text = "" + secondCount + ".";
        }

        if(secondCount >= 60)
        {
            secondCount = 0;
            minuteCount += 1;
        }

        if(minuteCount <= 9)
        {
            minuteBox.GetComponent<Text>().text = "0" +minuteCount + ":";
        }
        else
        {
            minuteBox.GetComponent<Text>().text = "" + minuteCount + ":";
        }
    }
}
