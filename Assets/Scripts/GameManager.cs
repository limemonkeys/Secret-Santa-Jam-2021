using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isTwoDActive = true;
    public bool canMove = false;
    public int presentsLeft;
    public GameObject Presents3d;

    void Start() {
        presentsLeft = 0;
        foreach (Transform child in Presents3d.transform)
        {
            presentsLeft += 1;
        }
    }

    public void AddPresent(int value, string name)
    {
        RemovePresents(name);
    }

    private void RemovePresents(string name) 
    {
        Destroy(GameObject.Find(name));
        UpdatePresents();
    }

    private void UpdatePresents() 
    {
        presentsLeft = 0;
        foreach (Transform child in Presents3d.transform)
        {
            presentsLeft += 1;
        }
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
