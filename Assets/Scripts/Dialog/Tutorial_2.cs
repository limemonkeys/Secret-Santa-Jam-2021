using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class Tutorial_2 : MonoBehaviour
{
    private Text messageText;
    private TextWriter.TextWriterSingle textWriterSingle;
    private AudioSource mewingAudioSource;
    private int messageIndex;
    public GameManager gameManager;
    public GameObject InGameUISwitchPerspective;
    public GameObject InGameUIRest;
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
                    "Looks like this pillar is a little too tall. Too bad...",
                    "Maybe a little 'change of perspective' might help.",
                    "I've added some new keys to your repertoire, I'll walk you through them!",
                    "You can now press the Left and Right arrows to change dimensions!",
                    "Right now being in Two Dimensions, we can't pass this. Maybe a solution exists in the Third Dimension!",
                    "I've also added an R key in case the dimensions become unstable or unsynced.",
                    "If you notice any funny business, a quick switch from one dimension to the other sometimes fixes things!",
                    "Anyway, get back to getting presents! Santa is waiting!"
                };
                if (messageIndex >= messageArray.Length)
                {
                    gameManager.setCanMove(true);
                    gameManager.setCanRestart(true);
                    gameManager.setCanRotate(true);
                    InGameUISwitchPerspective.SetActive(true);
                    InGameUIRest.SetActive(true);
                    InGameUI.SetActive(true);
                    Destroy(transform.gameObject);
                }
                else
                {
                    string message = messageArray[messageIndex];
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
        InGameUI.SetActive(false);
        gameManager.setCanMove(false);
        messageIndex = 0;
        // Write this message at a speed of 1 char per sec
        //TextWriter.AddWriter_Static(messageText, "This is going along for a very long time to test text size", 0.1f, true);
    }


}