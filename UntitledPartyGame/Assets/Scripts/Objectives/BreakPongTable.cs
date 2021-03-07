using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakPongTable : MonoBehaviour
{
    public float speedThreshold = 10f;
    LevelManager lv;
    // Start is called before the first frame update
    void Start()
    {
        lv = FindObjectOfType<LevelManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(gameObject.GetComponent<Rigidbody>().velocity.magnitude >= speedThreshold)
        {
            //Break table
            lv.PongComplete();
        }
    }
}
