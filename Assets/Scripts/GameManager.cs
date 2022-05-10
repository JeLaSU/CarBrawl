using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Declaring what's needed in unity
    public GameObject needle;  
    public CarController carController;
    //Degrees for the needle
    private float startPosition = 210f, endPosition = -20, desiredPoisiton;

    public float vechileSpeed;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Incrementing the carspeed to make the needle move as intended
        vechileSpeed = carController.speed*1.4f;
        UpdateNeedle();
        
    }
    private void UpdateNeedle()
    {   //How many degrees the needle can move
        desiredPoisiton = startPosition - endPosition; 
        //Temporary angle
        float temp = vechileSpeed / 180;
        //Rotating the needle in Z-axis
        needle.transform.eulerAngles = new Vector3(0, 0, (startPosition - temp * desiredPoisiton));
    }
}
