using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentPickup : MonoBehaviour
{
    public int value;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Get the game manager object and add the value of the current coin
            FindObjectOfType<GameManager>().AddPresent(value);

            // Remove coin from game
            Destroy(gameObject);
        }
    }
}
