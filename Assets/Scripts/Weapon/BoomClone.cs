using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomClone : MonoBehaviour
{
    public GameObject boomer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject clone;
            clone = Instantiate(boomer, new Vector2(transform.position.x, transform.position.y), transform.rotation) as GameObject;
        }
    }
}
