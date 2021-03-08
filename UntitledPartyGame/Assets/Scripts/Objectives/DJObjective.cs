using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DJObjective : MonoBehaviour
{
    public NPCAI ai;
    public LevelManager lv;
    bool notComplete = true;

    // Start is called before the first frame update
    void Start()
    {
        //ai = this.GetComponent<NPCAI>();
        //lv = FindObjectOfType<LevelManager>();
    }

    private void Update()
    {
        if (ai.currentState == NPCAI.FSMStates.Stun && notComplete)
        {
            lv.DJComplete();
            notComplete = false;
        }
    }
}
