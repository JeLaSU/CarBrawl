using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    public Weapon weapon;



    public void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Player"))
        {
            //target.GetComponent<TopDownCarController>().currentWeaponEquipped = weapon;

            target.transform.GetChild(4).GetComponent<SpriteRenderer>().sprite = weapon.currentWeaponSprite;

            target.transform.GetChild(4).GetComponent<Weapon>().pickedUp = true;

            Destroy(gameObject);
        }
    }


}
