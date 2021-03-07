using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakTVObjective : MonoBehaviour
{

    public int numHits = 5;

    LevelManager lv;

    // Start is called before the first frame update
    void Start()
    {
        lv = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        numHits--;
        if (numHits <= 0)
        {
            // Add shattered screen?
            lv.TVComplete();
        }
    }
}
