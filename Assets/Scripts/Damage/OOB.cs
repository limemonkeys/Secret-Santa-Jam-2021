using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OOB : MonoBehaviour
{
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public int lifeTotal = 3;

    public float invincibilityLength;
    private float invincibilityCounter;

    void Start() 
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (invincibilityCounter <= 0) 
            {
                // Get the game manager object and add the value of the current coin
                FindObjectOfType<UniversalMovement>().Respawn();
                lifeTotal -= 1;
                if (lifeTotal == 2)
                {
                    heart1.SetActive(false);
                }
                else if (lifeTotal == 1)
                {
                    heart2.SetActive(false);
                }
                else if (lifeTotal == 0)
                {
                    heart3.SetActive(false);
                    // END GAME
                }
                invincibilityCounter = invincibilityLength;
            }
            
        }
    }

    void Update() 
    {
        if (invincibilityCounter > 0) 
        {
            invincibilityCounter -= Time.deltaTime;
        }

    }
}
