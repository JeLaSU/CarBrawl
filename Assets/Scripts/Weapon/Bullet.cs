using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public float timer = 1f;
    //public GameObject hitEffect; //om man vill ha en explosion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AICar") || collision.gameObject.CompareTag("Scenery"))
        {
            Destroy(gameObject);
        }
    }
    public void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime; // timern räknas ner
        }
        else
        {
            Destroy(gameObject); // om timern är 0 förstörns bullet
        }
    }

}
