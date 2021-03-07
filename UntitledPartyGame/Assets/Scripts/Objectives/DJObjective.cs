using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DJObjective : MonoBehaviour
{

    LevelManager lv;

    // Start is called before the first frame update
    void Start()
    {
        lv = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHitbox"))
        {
            lv.DJComplete();
        }
    }
}
