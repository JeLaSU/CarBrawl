using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    public float speed;

    // Start is called before the first frame update
    void Awake()
    {
         rigidbody2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        speed = rigidbody2D.velocity.sqrMagnitude;
    }
}
