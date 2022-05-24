using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CountDownManager : MonoBehaviour
{
    public int countDownTime;
    public Text countDownDisplay;
    public TopDownCarController[] cars;
    bool startDriving = false;


    private void Start()
    {
        StartCoroutine(CountDownStart());

    }

    public IEnumerator CountDownStart()
    {
        while(countDownTime > 0)
        {
            countDownDisplay.text = countDownTime.ToString();

            yield return new WaitForSeconds(1f);

            countDownTime--;
        }

        countDownDisplay.text = "G0";

        TimerManager.instance.BeginGame();

        countDownDisplay.GetComponent<Text>().gameObject.SetActive(false);

        startDriving = true;

    }

    public void FixedUpdate()
    {
        StartGame();
    }
    public void StartGame()
    {
        if(startDriving)
        {
            List<TopDownCarController> carList = new List<TopDownCarController>();
            carList = cars.ToList();

            foreach (TopDownCarController car in carList)
            {
                car.accelerationFactor = 8f;
            }
        }
    }
}
