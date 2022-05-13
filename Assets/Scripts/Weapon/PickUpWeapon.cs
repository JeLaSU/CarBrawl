using System.Collections;
using System.Collections.Generic;
using Unity;

public class PickUpWeapon : MonoBehaviour
{
    public GunShooting weapon;
    public void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Car")
        {

        }
    }
}
