using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject About;

    public void PlayGame()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void AboutGame()
    {
        About.SetActive(true);
        About.SetActive(false);
    }
}
