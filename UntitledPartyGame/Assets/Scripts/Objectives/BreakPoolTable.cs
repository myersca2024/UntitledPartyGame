using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakPoolTable : MonoBehaviour
{
    public int numHits = 5;
    LevelManager lv;
    // Start is called before the first frame update
    void Start()
    {
        lv = FindObjectOfType<LevelManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Throwable") || collision.gameObject.CompareTag("PlayerPunch"))
        {
            --numHits;
            if (numHits <= 0)
            {
                lv.TableComplete();
            }
        }
    }
}
