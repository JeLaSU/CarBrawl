using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public float speed;

    // Start is called before the first frame update
    void Awake()
    {
         rigidbody2d = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        speed = rigidbody2d.velocity.sqrMagnitude;
    }
}
