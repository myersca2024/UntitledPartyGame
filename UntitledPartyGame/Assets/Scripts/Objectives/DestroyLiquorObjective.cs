using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLiquorObjective : MonoBehaviour
{

    LevelManager lv;

    // Start is called before the first frame update
    void Start()
    {
        lv = FindObjectOfType<LevelManager>();
    }

}
