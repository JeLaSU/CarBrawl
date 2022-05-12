using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    bool go; // används för att ändra Direction av vapnet

    GameObject player; // refererar till spelaren
    GameObject boomerang; // refererar till spelarens vapen

    Transform itemToRotate;

    Vector3 locationInFrontOfPlayer; // platsen framför spelaren som ska åka till

    void Start()
    {
        go = false; // så den inte returnas än

        player = GameObject.Find("Car"); // gameobjektet som retuneras
        boomerang = GameObject.Find("Katanaboomerang");

        boomerang.GetComponent<MeshRenderer>().enabled = false; // Sänger av meshrenederer så vapnet är osynligt (så man inte ser att det är en klon)

        itemToRotate = gameObject.transform.GetChild(0); // hittar vapnet som är child av empty object

        // ljusterar platsen framför spelaren, den sista biten bestämmer hur långt vapnet ska gå
        locationInFrontOfPlayer = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) + player.transform.forward * 10f;

        StartCoroutine(Boom()); // nu börjar promenaden
    }
    IEnumerator Boom()
    {
        go = true;
        yield return new WaitForSeconds(1.5f); // kan ändra tiden till det man själv vill
        go = false;
    }
    // Update is called once per frame
    void Update()
    {
        itemToRotate.transform.Rotate(0, Time.deltaTime * 500, 0); // roterar objektet

        if (go)
        {
            transform.position = Vector3.MoveTowards(transform.position, locationInFrontOfPlayer, Time.deltaTime * 40); // ändrar positionen av platsen framför spelaren
        }
        if (!go)
        {
            // kommer tillbaka till spelaren
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z), Time.deltaTime * 40);
        }
        if(!go && Vector3.Distance(player.transform.position, transform.position) < 1.5)
        {
            // när objektet är nära spelaren så blir det normala objectet synligt och klonen förstörs. 
            boomerang.GetComponent<MeshRenderer>().enabled = true;
            Destroy(this.gameObject);
        }
    }
}
