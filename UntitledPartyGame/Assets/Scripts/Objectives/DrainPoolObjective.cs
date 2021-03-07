using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainPoolObjective : MonoBehaviour
{
    public int numHits = 8;
    LevelManager lv;

    // Start is called before the first frame update
    void Start()
    {
        lv = FindObjectOfType<LevelManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        numHits--;
        if (numHits <= 0)
        {
            // Add shattered screen?
            lv.PoolComplete();
        }
    }
}
