using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardInfo : MonoBehaviour
{
    public Text positionText;
    public Text driverNameText;

    //Used for the LeaderboardUIHandler. It will change the position, by the amount of objects in the lapCounters List
    public void SetPositionText(string newPosition)
    {
        positionText.text = newPosition;
    }
    //Because the Unity doesn't like changing the positions all the time in the leaderboards
    //The function will change the name of the first and the last player.
    public void SetDriverNameText(string newDriverName)
    {
        driverNameText.text = newDriverName;
    }

}
