using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_2_Starter : MonoBehaviour
{
    public GameObject Tutorial2;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Tutorial2.SetActive(true);
            Destroy(GameObject.Find("TriggerTutorialTwo"));
        }
    }
}