using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTarget : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Works");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // When detected target and F pressed, destroy and save
        if (other.gameObject.tag == "ItemTarget" && Input.GetKeyDown(KeyCode.F))
        {
            Destroy(gameObject);
        }
    }

}
