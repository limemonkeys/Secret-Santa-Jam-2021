using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class UI_Assistant : MonoBehaviour
{
    private Text messageText;
    private TextWriter.TextWriterSingle textWriterSingle;
    private AudioSource mewingAudioSource;
    private int messageIndex;
    public GameObject PlayerAndSanta;
    public GameManager gameManager;

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
                    "Hello! I'm Tutorial Tabby!",
                    "I'm going to give you the run down on how to play!",
                    "You can move in 2d using A and D to move horizontally",
                    "You can also use Spacebar to jump.",
                    "You might see me again in the future. Have fun!"
                };
                if (messageIndex >= messageArray.Length)
                {
                    PlayerAndSanta.SetActive(true);
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
        gameManager.setCanMove(false);
        messageIndex = 0;
        // Write this message at a speed of 1 char per sec
        //TextWriter.AddWriter_Static(messageText, "This is going along for a very long time to test text size", 0.1f, true);
    }

    
}
