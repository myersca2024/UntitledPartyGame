using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainPoolObjective : MonoBehaviour
{
    public AudioClip poolDrainSFX;
    public int numHits = 8;
    public AudioClip popSFX;
    public AudioClip drainSFX;
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
            AudioSource.PlayClipAtPoint(popSFX, transform.position);
            AudioSource.PlayClipAtPoint(drainSFX, transform.position);
            AudioSource.PlayClipAtPoint(poolDrainSFX, gameObject.transform.position);
            lv.PoolComplete();
        }
    }
}
