using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PositionHandler : MonoBehaviour
{
    
    LeaderboardUIHandler leaderboardUIHandler;

    public List<CarLapCounter> carLapCounters = new List<CarLapCounter>();

    private void Awake()
    {
        //Get all Car lap counters in the scene
        CarLapCounter[] carLapCounterArray = FindObjectsOfType<CarLapCounter>();

        //Store the lap counters in a list
        carLapCounters = carLapCounterArray.ToList<CarLapCounter>();


        //Hookup the passed checkpoint event
        foreach (CarLapCounter lapCounters in carLapCounters)
        {
            lapCounters.onPassCheckpoint += OnPassCheckPoint;

        }
        //Get the leaderboard UI handler
        leaderboardUIHandler = FindObjectOfType<LeaderboardUIHandler>();
    }

    void Start()
    {
        //Ask the leaderboard handler to update the list
        leaderboardUIHandler.UpdateList(carLapCounters);
    }

    //This will trigger when a car is passing a checkpoint
    void OnPassCheckPoint(CarLapCounter carLapCounter)
    {   //Making the cars order in the list by a descending order, where the first car has the least checkpoints and the least time.
        //Sorting the cars by their checkpoint gathering and by the time they get to the checkpoint.
        //Making it a list in the end.
        carLapCounters = carLapCounters.OrderByDescending(s => s.GetNumberOfCheckPointsPassed()).ThenBy(s => s.GetTimeAtLastCheckPoint()).ToList();

        //Returns on which position the object of carlapcounter (list) has. Add it by one, because a list always starts at zero.
        //Get the carPosition.
        int carPosition = carLapCounters.IndexOf(carLapCounter) + 1;

        //Tells the lapcounter, on which position the car has.
        carLapCounter.SetCarPosition(carPosition);

        Debug.Log($"Event: Car { carLapCounter.gameObject.name} passed a checkpoint");

        //Ask the leaderboard handler to update the list
        leaderboardUIHandler.UpdateList(carLapCounters);
    }
}
