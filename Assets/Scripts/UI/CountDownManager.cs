using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CountDownManager : MonoBehaviour
{
    public static CountDownManager instance;

    public int countDownTime;
    public Text countDownDisplay;
    public TopDownCarController[] cars;
    public AStarLite[] carsAI;
    public CarAIHandler[] carAIHandler;

    public float timeToLast = 2;
    bool appearTime = false;


    private void Awake()
    {
        instance = this;
    }

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

        GameManager.instance.BeginGame();

        for (int i = 0; i < cars.Length; i++)
        {
            cars[i].BeginGame();
        }
        for (int i = 0; i < carsAI.Length; i++)
        {
            carsAI[i].BeginGame();
            carAIHandler[i].BeginGame();
        }

        yield return new WaitForSeconds(1f);

        countDownDisplay.GetComponent<Text>().gameObject.SetActive(false);

    }
    public void OpenCountDisplay()
    {
        countDownDisplay.GetComponent<Text>().gameObject.SetActive(true);

        TimerManager.instance.timerGoing = false;

        appearTime = true;

        
    }
    private void Update()
    {
        TextTimer();
    }
    private void TextTimer()
    {
        if (appearTime)
        {
            timeToLast -= Time.deltaTime;

            if (timeToLast > 0f)
            {
                countDownDisplay.GetComponent<Text>().text = "Complete";
            }
            else
            {
                countDownDisplay.GetComponent<Text>().text = TimerManager.instance.finalTime;
            }
        }
    }

}
