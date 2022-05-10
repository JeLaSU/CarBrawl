using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PositionHandler : MonoBehaviour
{
    public List<CarLapCounter> carLapCounters = new List<CarLapCounter>();

    void Start()
    {
        //Get all Car lap counters in the scene
        CarLapCounter[] carLapCounterArray = FindObjectsOfType<CarLapCounter>();

        //Store the lap counters in a list
        carLapCounters = carLapCounterArray.ToList<CarLapCounter>();

        
        //Hookup the passed checkpoint event
        foreach (CarLapCounter lapCounters in carLapCounters)
        {
            if (lapCounters.wrongCheckpoint)
            {
                lapCounters.onPassCheckpoint += OnPassCheckPoint;
            }
           
        }
            
    }

    //This will trigger when a car is passing a checkpoint
    void OnPassCheckPoint(CarLapCounter carLapCounter)
    {
        Debug.Log($"Event: Car { carLapCounter.gameObject.name} passed a checkpoint");
    }
}
