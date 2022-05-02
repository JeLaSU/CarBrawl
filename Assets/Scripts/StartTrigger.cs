using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrigger : MonoBehaviour
{
    public GameObject lapStartTrigger, lapEndTrigger;
    
    void OnTriggerEnter()
    {
        lapStartTrigger.SetActive(true);
        lapEndTrigger.SetActive(false);

        Debug.Log("You crossed the line");
    }

    
}
