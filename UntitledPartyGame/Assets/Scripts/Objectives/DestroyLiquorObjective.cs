using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLiquorObjective : MonoBehaviour
{

    public int numBottles = 0;

    LevelManager lv;

    // Start is called before the first frame update
    void Start()
    {
        lv = FindObjectOfType<LevelManager>();
    }

    public void IncreaseBottles()
    {
        numBottles++;
    }

    public void DecreaseBottles()
    {
        numBottles--;
        if (numBottles <= 0)
        {
            lv.LiquorComplete();
        }
    }
}
