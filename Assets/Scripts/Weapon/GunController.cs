using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    
    void Start()
    {
        
    }

    void Update()
    {
        faceMouse();
        
    }
    void faceMouse() // gör så att vapnets ände följer musen
    {
        Vector2 mousePosition = Input.mousePosition; 
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); // initierar mousePosition till musen 

        Vector2 direction = new Vector3(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y); 
        transform.up = direction; // eftersom det är 2D så blir up, direction
    }
    
}
