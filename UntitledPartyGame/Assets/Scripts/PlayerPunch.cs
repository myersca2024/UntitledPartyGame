using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{

    public Animator anim;
    public GameObject hitbox;
    public int hitboxDuration = 60;

    void Start()
    {
        hitbox.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetInteger("animState", 2);
            hitbox.SetActive(true);
        }
        if(hitbox.activeSelf)
        {
            hitboxDuration--;
        }
        if(hitboxDuration == 0)
        {
            hitbox.SetActive(false);
            hitboxDuration = 60;
            anim.SetInteger("animState", 0);
        }
    }
}
