using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isTwoDActive = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setisTwoDActive(bool isTwoDActive) {
        this.isTwoDActive = isTwoDActive;
    }

    public bool isTwoD() {
        return isTwoDActive;
    
    }
}
