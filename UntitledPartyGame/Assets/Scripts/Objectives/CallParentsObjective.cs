using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallParentsObjective : MonoBehaviour
{

    LevelManager lv;

    // Start is called before the first frame update
    void Start()
    {
        lv = FindObjectOfType<LevelManager>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Display Interact Dialogue?

            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(gameObject.GetComponent<BoxCollider>());
                lv.ParentsComplete();
            }
        }    
    }
}
