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

        yield return new WaitForSeconds(1f);

        countDownDisplay.GetComponent<Text>().gameObject.SetActive(false);

    }
}
