using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpillPunchObjective : MonoBehaviour
{

    LevelManager lv;

    // Start is called before the first frame update
    void Start()
    {
        lv = FindObjectOfType<LevelManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Ground"))
        {
            gameObject.transform.Find("Punch").gameObject.SetActive(false);

            lv.PunchComplete();
        }
    }
}
