using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OOB : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Get the game manager object and add the value of the current coin
            FindObjectOfType<UniversalMovement>().Respawn();
        }
    }
}
