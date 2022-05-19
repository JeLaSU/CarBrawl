using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PickUpWeapon : MonoBehaviour
{
    public Weapon weapon;
    //object on speedometer
    public Image weaponSpeed;

    private Color tmp;

    private void Awake()
    {
        weaponSpeed.GetComponent<Image>();
        tmp = weaponSpeed.GetComponent<Image>().color;
        tmp.a = 0.6f;

    }

    public void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Player"))
        {
            //the weapon on the car changes on pickup.
            target.GetComponent<TopDownCarController>().currentWeaponEquipped = weapon;

            //Change sprite on the car.
            target.transform.GetChild(4).GetComponent<SpriteRenderer>().sprite = weapon.currentWeaponSprite;

            //Change sprite on speedometer.
            weaponSpeed.sprite = weapon.currentWeaponSprite;
            weaponSpeed.color = tmp;
            

            target.transform.GetChild(4).GetComponent<Weapon>().pickedUp = true;

            Destroy(gameObject);
        }
    }


}
