using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxHurtPlayer : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Please :C");
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Health>().takeDamage(damage);
        }
    }
}
