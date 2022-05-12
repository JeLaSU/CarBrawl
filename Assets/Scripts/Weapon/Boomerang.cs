using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    bool go; // anv�nds f�r att �ndra Direction av vapnet

    GameObject player; // refererar till spelaren
    GameObject boomerang; // refererar till spelarens vapen

    Transform itemToRotate;

    Vector3 locationInFrontOfPlayer; // platsen framf�r spelaren som ska �ka till

    void Start()
    {
        go = false; // s� den inte returnas �n

        player = GameObject.Find("Car"); // gameobjektet som retuneras
        boomerang = GameObject.Find("Katanaboomerang");

        boomerang.GetComponent<MeshRenderer>().enabled = false; // S�nger av meshrenederer s� vapnet �r osynligt (s� man inte ser att det �r en klon)

        itemToRotate = gameObject.transform.GetChild(0); // hittar vapnet som �r child av empty object

        // ljusterar platsen framf�r spelaren, den sista biten best�mmer hur l�ngt vapnet ska g�
        locationInFrontOfPlayer = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) + player.transform.forward * 10f;

        StartCoroutine(Boom()); // nu b�rjar promenaden
    }
    IEnumerator Boom()
    {
        go = true;
        yield return new WaitForSeconds(1.5f); // kan �ndra tiden till det man sj�lv vill
        go = false;
    }
    // Update is called once per frame
    void Update()
    {
        itemToRotate.transform.Rotate(0, Time.deltaTime * 500, 0); // roterar objektet

        if (go)
        {
            transform.position = Vector3.MoveTowards(transform.position, locationInFrontOfPlayer, Time.deltaTime * 40); // �ndrar positionen av platsen framf�r spelaren
        }
        if (!go)
        {
            // kommer tillbaka till spelaren
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z), Time.deltaTime * 40);
        }
        if(!go && Vector3.Distance(player.transform.position, transform.position) < 1.5)
        {
            // n�r objektet �r n�ra spelaren s� blir det normala objectet synligt och klonen f�rst�rs. 
            boomerang.GetComponent<MeshRenderer>().enabled = true;
            Destroy(this.gameObject);
        }
    }
}
