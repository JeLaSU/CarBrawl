using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName ="", menuName ="")]
public class Weapon : ScriptableObject
{
    public Sprite currentWeaponSprite;

    public GameObject bulletPrefab;
    public float fireRate = 1;
    public int damage = 2;

    public void Shoot()
    {
        Instantiate(bulletPrefab, GameObject.Find("FirePoint").transform.position, Quaternion.identity);
    }
}
