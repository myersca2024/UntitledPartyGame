﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoloCupBehavior : MonoBehaviour
{

    SoloCupObjective sco;
    bool onGround = false;

    // Start is called before the first frame update
    void Start()
    {
        sco = FindObjectOfType<SoloCupObjective>();
        sco.IncreaseCups();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Floor"))
        {
            // kill cups?
            if (!onGround)
            {
                sco.DecreaseCups();
            }
        }
    }
}
