using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public float timer = 1f;
    public GameObject hitEffect; //om man vill ha en explosion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity); // n�r effektenspriten ska synas
        Destroy(effect, 5f); //effektspriten ska f�rsvinna fr�n spelet
        Destroy(gameObject); // bullet ska f�rsvinna fr�n spelet

    }
    public void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime; // timern r�knas ner
        }
        else
        {
            Destroy(gameObject); // om timern �r 0 f�rst�rns bullet
        }
    }

}
