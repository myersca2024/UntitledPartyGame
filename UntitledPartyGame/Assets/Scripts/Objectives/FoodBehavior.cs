using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBehavior : MonoBehaviour
{

    FoodObjective fo;
    bool onGround = false;

    // Start is called before the first frame update
    void Start()
    {
        fo = FindObjectOfType<FoodObjective>();
        fo.IncreaseFood();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Ground"))
        {
            // destroy food??? 
            if (!onGround)
            {
                fo.DecreaseFood();
                onGround = true;
            }
        }
    }
}
