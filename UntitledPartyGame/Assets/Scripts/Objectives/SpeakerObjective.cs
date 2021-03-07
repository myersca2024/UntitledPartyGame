using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerObjective : MonoBehaviour
{

    LevelManager lv;
    public int numSpeakers = 0;

    // Start is called before the first frame update
    void Start()
    {
        lv = FindObjectOfType<LevelManager>();
    }

    public void IncreaseSpeaker()
    {
        numSpeakers++;
    }    

    public void DecreaseSpeaker()
    {
        numSpeakers--;
        if (numSpeakers <= 0)
        {
            lv.SpeakerComplete();
        }
    }
}
