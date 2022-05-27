using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject needle;
    public CarController carController;
    public Text speedText;
    private float startPosition = 210f, endPosition = -20, desiredPoisiton;
    public float vechileSpeed;
    


    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        vechileSpeed = carController.speed*1.4f;
        UpdateNeedle();
        
    }
    private void Update()
    {
        GoBackToMenuOnInput();
    }
    private void UpdateNeedle()
    {
        desiredPoisiton = startPosition - endPosition;
        float temp = vechileSpeed / 180;
        needle.transform.eulerAngles = new Vector3(0, 0, (startPosition - temp * desiredPoisiton));

    }
    public void BeginGame()
    {
        TimerManager.instance.BeginGame();
    }
    public void GoBackToMenuOnInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            TimerManager.instance.RestartGame();
        }
    }


}
