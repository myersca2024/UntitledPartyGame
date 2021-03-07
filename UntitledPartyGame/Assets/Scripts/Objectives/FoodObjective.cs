using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodObjective : MonoBehaviour
{

    LevelManager lv;
    public int numFood = 0;

    // Start is called before the first frame update
    void Start()
    {
        lv = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseFood()
    {
        numFood++;
    }

    public void DecreaseFood()
    {
        numFood--;
        if (numFood <= 0)
        {
            lv.FoodComplete();
        }
    }
}
