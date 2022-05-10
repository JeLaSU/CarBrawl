using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public float timer = 1f;
    public GameObject hitEffect; //om man vill ha en explosion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity); // när effektenspriten ska synas
        Destroy(effect, 5f); //effektspriten ska försvinna från spelet
        Destroy(gameObject); // bullet ska försvinna från spelet

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
