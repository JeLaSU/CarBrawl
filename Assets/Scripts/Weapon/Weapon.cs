using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    //current sprite
    public Sprite currentWeaponSprite;
    
    //Place the weapon shoots the bullet from
    public Transform firePoint;
    //adding the bullet.
    public GameObject bulletPrefab;

    //speed
    public float bulletForce = 20f;
    //cooldown
    public float timer = 1f;
    //bool for pick up, if its picked up, it will shoot
    public bool pickedUp = false;
  

    void Update()
    {
        //press m1 to shoot with a 1 sec timer
        if (Input.GetButtonDown("Fire1") && timer < 0) 
        {
            //bool for picked up
            if(pickedUp)
                Shoot();
                timer = 1f;
        }
        else
        {
            //timer ticking down every tick
            timer -= Time.deltaTime;
        }

    }

    //making a bullet based on a prefab and a firepoint
    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

   
}
