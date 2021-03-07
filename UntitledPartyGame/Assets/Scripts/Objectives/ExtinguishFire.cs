using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguishFire : MonoBehaviour
{
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
            //Visibly extinguish, sounds
            lv.FireComplete();
        }
    }

}
