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
                    "Hmm, I can't think what I want to put on my letter to Santa...",
                    "I've been feeling quite nauseated from the dimenstion switching.",
                    "Huh? Who's that?",
                    "Wow! The real Santa Claus!",
                    "Oh, Santa Claws. Still exciting.",
                    "Oh no! I can help find your presents!",
                    "Deal! See you soon Santa Claws!",
                };

                string message = messageArray[Random.Range(0, messageArray.Length)];
                mewingAudioSource.Play();
                textWriterSingle = TextWriter.AddWriter_Static(messageText, message, 0.05f, true, true, StopMewingSound);
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
        // Write this message at a speed of 1 char per sec
        //TextWriter.AddWriter_Static(messageText, "This is going along for a very long time to test text size", 0.1f, true);
    }

    
}
