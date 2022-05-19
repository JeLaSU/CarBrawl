using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject needle;
    public CarController carController;
    public Text speedText;
    private float startPosition = 210f, endPosition = -20, desiredPoisiton;
    public float vechileSpeed;

    // Update is called once per frame
    void FixedUpdate()
    {
        vechileSpeed = carController.speed*1.4f;
        UpdateNeedle();

        
    }
    private void UpdateNeedle()
    {
        desiredPoisiton = startPosition - endPosition;
        float temp = vechileSpeed / 180;
        needle.transform.eulerAngles = new Vector3(0, 0, (startPosition - temp * desiredPoisiton));

    }
    

}
