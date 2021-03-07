using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakTVObjective : MonoBehaviour
{

    public int numHits = 5;
    public AudioClip breakTVSFX;
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
            AudioSource.PlayClipAtPoint(breakTVSFX, gameObject.transform.position);
            lv.TVComplete();
        }
    }
}
