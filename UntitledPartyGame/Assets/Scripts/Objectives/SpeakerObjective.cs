using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerObjective : MonoBehaviour
{

    LevelManager lv;
    public int numSpeakers = 0;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
            player.GetComponent<PlayerController>().speakersDestroyed = true;
            lv.SpeakerComplete();
        }
    }
}
