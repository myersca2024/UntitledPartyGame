using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public int startingHealth = 100;
    public int damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        health = startingHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hitbox")
        {
            takeDamage(damage);
        }
    }

    void takeDamage(int damage)
    {
        Mathf.Clamp(health - damage, 0, 100);
        if(health == 0)
        {
            gameOver();
        }
    }

    void gameOver()
    {

    }
}
