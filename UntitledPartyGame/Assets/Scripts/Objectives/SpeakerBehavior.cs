using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerBehavior : MonoBehaviour
{

    SpeakerObjective so;
    bool onGround = false;

    // Start is called before the first frame update
    void Start()
    {
        so = FindObjectOfType<SpeakerObjective>();
        so.IncreaseSpeaker();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Kill speaker somehow?
            if (!onGround)
            {
                so.DecreaseSpeaker();
                onGround = true;
            }
        }
    }
}
