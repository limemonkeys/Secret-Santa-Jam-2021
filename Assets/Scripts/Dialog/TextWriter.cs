using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{
    private static TextWriter instance;

    private List<TextWriterSingle> textWriterSingleList;

    public void Awake() 
    {
        instance = this;
        textWriterSingleList = new List<TextWriterSingle>();
    }

    public static TextWriterSingle AddWriter_Static(Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, bool removeWriterBeforeAdd, Action onComplete)
    {
        if (removeWriterBeforeAdd) 
        {
            instance.RemoveWriter(uiText);
        }
        return instance.AddWriter(uiText, textToWrite, timePerCharacter, invisibleCharacters, onComplete);
    }

    public TextWriterSingle AddWriter(Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, Action onComplete)
    {
        TextWriterSingle textWriterSingle = new TextWriterSingle(uiText, textToWrite, timePerCharacter, invisibleCharacters, onComplete);
        textWriterSingleList.Add(textWriterSingle);
        return textWriterSingle;
    }

    public static void RemoveWriter_Static(Text uiText) 
    {
        instance.RemoveWriter(uiText);
    }

    public void RemoveWriter(Text uiText) 
    {
        for (int i = 0; i < textWriterSingleList.Count; i++)
        {
            if (textWriterSingleList[i].GetUIText() == uiText) 
            {
                textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
    }

    private void Update() 
    {
        // Debug.Log(textWriterSingleList.Count);
        for (int i = 0; i < textWriterSingleList.Count; i++) 
        {
            bool destroyInstance = textWriterSingleList[i].Update();
            if (destroyInstance) 
            {
                // Remove text from list once finished
                textWriterSingleList.RemoveAt(i);
                i--;
            }
        }

    }

    // Represents a single TextWriter Instance

    public class TextWriterSingle 
    {
        private Text uiText;
        private string textToWrite;
        private int characterIndex;
        private float timePerCharacter;
        private float timer;
        private bool invisibleCharacters;
        private Action onComplete;

        public TextWriterSingle(Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, Action onComplete)
        {
            this.uiText = uiText;
            this.textToWrite = textToWrite;
            this.timePerCharacter = timePerCharacter;
            this.invisibleCharacters = invisibleCharacters;
            this.onComplete = onComplete;
            characterIndex = 0;
        }

        // Returns true on complete
        public bool Update()
        {
            timer -= Time.deltaTime;
            while (timer <= 0f)
            {
                // Display the next character
                timer += timePerCharacter;
                characterIndex++;
                string text = textToWrite.Substring(0, characterIndex);
                if (invisibleCharacters)
                {
                    text += "<color=#00000000>" + textToWrite.Substring(characterIndex) + "</color>";
                }


                uiText.text = text;

                if (characterIndex >= textToWrite.Length)
                {
                    // Finished
                    if (onComplete != null) 
                    {
                        onComplete();
                    }
                    return true;
                }
            }
            return false;
        }

        public Text GetUIText() 
        {
            return uiText;
        }

        public bool IsActive() 
        {
            return characterIndex < textToWrite.Length;
        }

        public void WriteAllAndDestroy() 
        {
            uiText.text = textToWrite;
            characterIndex = textToWrite.Length;
            if (onComplete != null)
            {
                onComplete();
            }
            TextWriter.RemoveWriter_Static(uiText);
        }
    }
}

/*
 *  Previous code for single text writer
 * 
 * 
    private Text uiText;
    private string textToWrite;
    private int characterIndex;
    private float timePerCharacter;
    private float timer;
    private bool invisibleCharacters; 
    
    public void AddWriter(Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters) 
    {
        this.uiText = uiText;
        this.textToWrite = textToWrite;
        this.timePerCharacter = timePerCharacter;
        this.invisibleCharacters = invisibleCharacters;
        characterIndex = 0;
    }

    
    private void Update() 
    {
        if (uiText != null) 
        {
            timer -= Time.deltaTime;
            while (timer <= 0f) 
            {
                // Display the next character
                timer += timePerCharacter;
                characterIndex++;
                string text = textToWrite.Substring(0, characterIndex);
                if (invisibleCharacters) 
                {
                    text += "<color=#00000000>" + textToWrite.Substring(characterIndex) + "</color>";
                }


                uiText.text = text;

                if (characterIndex >= textToWrite.Length) 
                {
                    // Finished
                    uiText = null;
                    return;
                }
            }
        }
    }
    
*/