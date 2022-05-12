using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    float rotate = 90;
    
    // Update is called once per frame
    public void FixedUpdate()
    {
        rotate--;
        RotationHandler();
    }

    public void RotationHandler()
    {
        transform.eulerAngles = new Vector3(0, 0, rotate);
    }
}
