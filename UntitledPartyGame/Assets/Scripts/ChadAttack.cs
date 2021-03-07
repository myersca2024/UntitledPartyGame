using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChadAttack : MonoBehaviour, INPCAttack
{
    public Animator anim;
    public GameObject hitBox;
    public static bool inMiddleOfAttack = false;
    public AudioClip chadAttackSFX;

    void Start()
    {
        hitBox.SetActive(false);
    }

    void INPCAttack.Attack()
    {
        AudioSource.PlayClipAtPoint(chadAttackSFX, transform.position);
        if (!inMiddleOfAttack)
        {
            hitBox.SetActive(true);
            Invoke("DeactivateHitbox", 0.7f);
            inMiddleOfAttack = true;
            Debug.Log("Hitbox activated");
        }
    }
    void DeactivateHitbox()
    {
        hitBox.SetActive(false);
        inMiddleOfAttack = false;
        Debug.Log("Hitbox deactivated");
    }
}
