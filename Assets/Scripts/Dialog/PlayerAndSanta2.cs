using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using UnityEngine.SceneManagement;

public class PlayerAndSanta2 : MonoBehaviour
{
    private Text messageText;
    private TextWriter.TextWriterSingle textWriterSingle;
    private AudioSource mewingAudioSource;
    private int messageIndex;

    private string[] playerVsSantaIndex = new string[] {"S", "S", "P" };

    public GameManager gameManager;
    public GameObject Santa;
    public GameObject Player;
    public GameObject SantaArrow;
    public GameObject PlayerArrow;
    public GameObject InGameUI;

    public AudioSource mainAudio;

    private void Awake()
    {
        messageText = transform.Find("message").Find("messageText").GetComponent<Text>();
        mewingAudioSource = transform.Find("mewingSound").GetComponent<AudioSource>();

        transform.Find("message").GetComponent<Button_UI>().ClickFunc = () =>
        {
            if (textWriterSingle != null && textWriterSingle.IsActive())
            {
                // Test writer is active
                textWriterSingle.WriteAllAndDestroy();
            }
            else
            {
                string[] messageArray = new string[]{
                    "Well done! Well done!",
                    "So? What is your wish?",
                    "I wish to be in only one dimension. All this flipping is making me sick!"
                };
                if (messageIndex >= messageArray.Length)
                {
                    SceneManager.LoadScene("MainMenu");
                }
                else
                {
                    string message = messageArray[messageIndex];
                    string playerOrSanta = playerVsSantaIndex[messageIndex];
                    if (playerOrSanta == "S")
                    {
                        PlayerArrow.SetActive(false);
                        Santa.SetActive(true);
                        Player.SetActive(false);
                        SantaArrow.SetActive(true);
                    }
                    else if (playerOrSanta == "P")
                    {
                        SantaArrow.SetActive(false);
                        Santa.SetActive(false);
                        Player.SetActive(true);
                        PlayerArrow.SetActive(true);

                    }

                    mewingAudioSource.Play();
                    textWriterSingle = TextWriter.AddWriter_Static(messageText, message, 0.05f, true, true, StopMewingSound);
                    messageIndex += 1;
                }


            }



        };
    }

    private void StartMewingSound()
    {
        mewingAudioSource.Play();
    }

    private void StopMewingSound()
    {
        mewingAudioSource.Stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        mainAudio.Stop();
        gameManager.setCanMove(false);
        messageIndex = 0;
        // Write this message at a speed of 1 char per sec
        //TextWriter.AddWriter_Static(messageText, "This is going along for a very long time to test text size", 0.1f, true);
    }


}
