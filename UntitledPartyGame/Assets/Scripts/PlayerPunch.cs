using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{

    public Animator anim;
    public GameObject hitbox;
    public int hitboxDuration = 30;

    void Start()
    {
        hitbox.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("PUNCH!");
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
            hitboxDuration = 30;
            anim.SetInteger("animState", 0);
        }
    }
}
