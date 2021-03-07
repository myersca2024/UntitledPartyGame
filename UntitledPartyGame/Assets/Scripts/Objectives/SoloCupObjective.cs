using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoloCupObjective : MonoBehaviour
{

    public int numCups = 0;

    LevelManager lv;

    // Start is called before the first frame update
    void Start()
    {
        lv = FindObjectOfType<LevelManager>();
    }

    public void IncreaseCups()
    {
        numCups++;
    }

    public void DecreaseCups()
    {
        numCups--;
        if (numCups <= 0)
        {
            lv.SoloComplete();
        }
    }
}
