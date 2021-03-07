using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakPongTable : MonoBehaviour
{
    public float speedThreshold = 10f;
    public AudioClip pongTableBreakSFX;
    LevelManager lv;
    // Start is called before the first frame update
    void Start()
    {
        lv = FindObjectOfType<LevelManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rbTest;
        if (collision.gameObject.TryGetComponent<Rigidbody>(out rbTest))
        {
            if (gameObject.GetComponent<Rigidbody>().velocity.magnitude >= speedThreshold || collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude >= speedThreshold)
            {
                //Break table
                AudioSource.PlayClipAtPoint(pongTableBreakSFX, gameObject.transform.position);
                lv.PongComplete();
            }
        }
    }
}
