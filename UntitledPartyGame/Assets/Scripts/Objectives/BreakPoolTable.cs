using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakPoolTable : MonoBehaviour
{
    public int numHits = 5;
    public AudioClip breakPoolTableSFX;
    LevelManager lv;
    // Start is called before the first frame update
    void Start()
    {
        lv = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Throwable") || other.CompareTag("PlayerHitbox"))
        {
            --numHits;
            if (numHits <= 0)
            {
                AudioSource.PlayClipAtPoint(breakPoolTableSFX, Camera.main.transform.position);
                lv.TableComplete();
            }
        }
    }
}
