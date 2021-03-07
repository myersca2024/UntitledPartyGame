using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceAttack : MonoBehaviour, INPCAttack
{
    public GameObject bullet;
    public float bulletSpeed;
    public float reloadTime = 1f;

    private bool canShoot = true;
    private float currentReload;

    void Start()
    {
        currentReload = reloadTime + 0.3f;
    }

    void Update()
    {
        currentReload -= Time.deltaTime;
        if (currentReload <= 0)
        {
            canShoot = true;
        }
    }

    void INPCAttack.Attack()
    {
        if (canShoot)
        {
            GameObject newBullet = Instantiate(bullet, transform.position + transform.forward, transform.rotation);
            Rigidbody rb = newBullet.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * bulletSpeed, ForceMode.VelocityChange);
            currentReload = reloadTime;
            canShoot = false;
        }
    }
}
