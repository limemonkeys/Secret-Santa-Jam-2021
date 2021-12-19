using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentPickup : MonoBehaviour
{
    public int value;

    public GameManager gameManager;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Get the game manager object and add the value of the current present
            gameManager = FindObjectOfType<GameManager>();
            gameManager.AddPresent(value, gameObject.name);

            // Remove present(s) from game
        }
    }

}
