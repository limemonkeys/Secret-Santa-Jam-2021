using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isTwoDActive = true;
    public bool canMove = false;
    public bool canRotate = false;
    public bool canRestart = false;
    public int presentsLeft;
    public GameObject Presents3d;
    public Text PresentsLeftNumber;
    public GameObject PlayerAndSanta2;

    public AudioSource jumpSFX;

    void Start() {
        presentsLeft = 0;
        foreach (Transform child in Presents3d.transform)
        {
            presentsLeft += 1;
        }
        PresentsLeftNumber.text = presentsLeft.ToString();
    }

    public void AddPresent(int value, string name)
    {
        jumpSFX.Play();
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
        PresentsLeftNumber.text = presentsLeft.ToString();
        if (presentsLeft <= 0) 
        {
            PlayerAndSanta2.SetActive(true);
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

    public void setCanRestart(bool canRestart) 
    {
        this.canRestart = canRestart;
    }

    public bool isCanRestart() 
    {
        return canRestart;
    }

    public void setCanRotate(bool canRotate)
    {
        this.canRotate = canRotate;
    }

    public bool isCanRotate()
    {
        return canRotate;
    }

}
