using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class PlayerAndSanta : MonoBehaviour
{
    private Text messageText;
    private TextWriter.TextWriterSingle textWriterSingle;
    private AudioSource mewingAudioSource;
    private int messageIndex;

    private string[] playerVsSantaIndex = new string[] { "P", "P", "P", "S", "P", "S", "P", "S", "P", "S", "S", "P", "S", "S", "P" };

    public GameManager gameManager;
    public GameObject Santa;
    public GameObject Player;
    public GameObject SantaArrow;
    public GameObject PlayerArrow;
    public GameObject InGameUI;

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
                    "Hey Player, I'm Persian the Player! You're going to be me today.",
                    "I have to write my letter to Santa, but I'm not sure what I want yet...",
                    "Maybe we'll figure out what I want by the end of the-",
                    "Ho! Ho! Ho!",
                    "Could it be! Santa Claus?!",
                    "Close. Santa Claws.",
                    "Oh, well great to see you either way! Could I tell you what I want for Christmas?",
                    "I'd love to entertain that, but I'm short on time. I'm in quite a pickle.",
                    "What's the matter?",
                    "I was practicing my route for Christmas, but I snagged my bag of goodies on my way out",
                    "I dropped a bunch of presents along my route! I'm not sure I'll be able to collect them in time for Christmas!",
                    "It's okay Santa! I'll help you find them!",
                    "Really?! Oh Persian you're a life saver! With your help you can save Christmas!",
                    "If you manage to find all the presents, I'll get whatever you want for Christmas.",
                    "Deal! See you soon Santa!"
                };
                if (messageIndex >= messageArray.Length)
                {
                    gameManager.setCanMove(true);
                    InGameUI.SetActive(true);
                    Destroy(transform.gameObject);
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
        gameManager.setCanMove(false);
        messageIndex = 0;
        // Write this message at a speed of 1 char per sec
        //TextWriter.AddWriter_Static(messageText, "This is going along for a very long time to test text size", 0.1f, true);
    }


}
