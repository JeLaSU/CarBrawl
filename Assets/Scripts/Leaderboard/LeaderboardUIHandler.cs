using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardUIHandler : MonoBehaviour
{
    //choose the gameobject in unity
    public GameObject leaderboardItemPrefab;

    //Array for many rows
    LeaderboardInfo[] leaderboardInfo;

    private void Awake()
    {
        //calling the rows of the leaderboard
        VerticalLayoutGroup leaderboardLayoutGroup = GetComponentInChildren<VerticalLayoutGroup>();

        CarLapCounter[] carLapCounters = FindObjectsOfType<CarLapCounter>();

        //leaderboard will have rows dependent on how many cars there is
        leaderboardInfo = new LeaderboardInfo[carLapCounters.Length];

        for (int i = 0; i < carLapCounters.Length; i++)
        {
            //Instead of changing existing texts and positions, make a new one.
            GameObject leaderBoardInfoGameObject = Instantiate(leaderboardItemPrefab, leaderboardLayoutGroup.transform);
            
            //Based on how many car there is in a list, set as many rows
            leaderboardInfo[i] = leaderBoardInfoGameObject.GetComponent<LeaderboardInfo>();

            leaderboardInfo[i].SetPositionText($"{i + 1}.");

        }
    }
    public void UpdateList(List<CarLapCounter> lapCounters)
    {
        //Create the leaderboard items
        for (int i = 0; i < lapCounters.Count; i++)
        {
            //Setting the names of the players from lapcounters, which comes from the cars that are chosen in unity.
            leaderboardInfo[i].SetDriverNameText(lapCounters[i].gameObject.name);
        }
    }
}
