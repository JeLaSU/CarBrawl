using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject needle;
    public CarController carController;
    private float startPosition = 210f, endPosition = -20, desiredPoisiton;

    public float vechileSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        vechileSpeed = carController.speed;
        vechileSpeed = vechileSpeed / 2;
        UpdateNeedle();
    }
    private void UpdateNeedle()
    {
        desiredPoisiton = startPosition - endPosition;
        float temp = vechileSpeed / 180;
        needle.transform.eulerAngles = new Vector3(0, 0, (startPosition - temp * desiredPoisiton));
    }
}
