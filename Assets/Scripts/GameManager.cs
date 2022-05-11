using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject needle;
    public CarController carController;
    public GameObject speedLabelTemplate;
    public Text speedText;
    private float startPosition = 210f, endPosition = -20, desiredPoisiton;

    public float vechileSpeed;

    float maxSpeed = 200f;
    private Transform speedLabelTemplateTransform;
    int labelAmount = 5;

    private void Awake()
    {

        speedLabelTemplateTransform = speedLabelTemplate.transform;
        speedLabelTemplateTransform.gameObject.SetActive(false);
        CreateLabel();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void CreateLabel()
    {
        
        desiredPoisiton = startPosition - endPosition;

        for (int i = 0; i <= labelAmount; i++)
        {
            Transform speedLabelTransform = Instantiate(speedLabelTemplateTransform, transform);
            float labelSpeedNormalized = (float)i / labelAmount;
            float speedLabelAngle = startPosition - labelSpeedNormalized * desiredPoisiton;
            speedLabelTransform.eulerAngles = new Vector3(0, 0, speedLabelAngle);
            speedText.GetComponent<Text>().text = Mathf.RoundToInt(labelSpeedNormalized * maxSpeed).ToString();
            speedLabelTransform.gameObject.SetActive(true);
        }
    }

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
