using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isTwoDActive = true;
    public bool canMove = false;
    public int presents = 0;

    public void AddPresent(int value)
    {
        presents += value;
    }

    public void setisTwoDActive(bool isTwoDActive) {
        this.isTwoDActive = isTwoDActive;
    }

    public bool isTwoD() {
        return isTwoDActive;
    
    }

    public bool isCanMove()
    {
        return canMove;
    }

    public void setCanMove(bool canMove) 
    {
        this.canMove = canMove;
    }

    
}
