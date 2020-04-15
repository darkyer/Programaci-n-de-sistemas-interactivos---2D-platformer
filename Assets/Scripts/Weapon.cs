using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Animator anim;
    public Transform bulletsParent;

    void Update()
    {
        if (Input.GetButtonDown("Fire"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        anim.SetTrigger("isShooting");
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation, bulletsParent);
    }
}
