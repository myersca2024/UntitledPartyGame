using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChadAttack : MonoBehaviour, INPCAttack
{
    public Animator anim;
    public GameObject hitBox;

    void Start()
    {
        hitBox.SetActive(false);
    }

    void INPCAttack.Attack()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("m_melee_combat_attack_A"))
        {
            float animTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (animTime <= 1)
            {
                hitBox.SetActive(true);
            }
            else
            {
                hitBox.SetActive(false);
            }
        }
    }
}
