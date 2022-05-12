using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f; 
    public float timer = 1f;


    void Start()
    {
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && timer < 0)
        {
            Shoot();
            timer = 1f;
        }
        else
        {
            timer -= Time.deltaTime;
        }
        
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
