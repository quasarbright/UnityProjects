using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LettersHandler : MonoBehaviour
{

    public List<Text> texts;
    static string alphabet = "abcdefghijklmnopqrstuvwxyz";
    string vowels = "aeiou";
    string consonants = "bcdfghjklmnpqrstvwxyz";
    List<char> usableLetters = new List<char>();

    public InputField input;

    string word = "";

    // Start is called before the first frame update
    void Start()
    {
        // randomize characters on startup
        System.Random rand = new System.Random();
        foreach (Text text in texts)
        {
            int index = rand.Next(0, 26);
            char letter = alphabet[index];
            usableLetters.Add(letter);
            text.text = letter.ToString();
        }
        // add input change listener
        input.onValueChanged.AddListener(delegate { OnChange(); });
        input.onEndEdit.AddListener(delegate { OnEnd(); });
        input.Select();
        input.ActivateInputField();
    }

    // Update is called once per frame
    void OnChange()
    {
        string newWord = input.text;
        if(!IsValid(newWord)){
            input.text = word;
        } else {
            input.text = newWord;
            word = newWord;
        }
    }

    void OnEnd()
    {
        input.text = "";
        input.Select();
        input.ActivateInputField();
    }

    // is the word valid given the list of usable letters?
    bool IsValid(string word)
    {
        List<char> lettersLeft = new List<char>(usableLetters);
        foreach (char letter in word)
        {
            if (!lettersLeft.Remove(letter))
            {
                return false;
            }
        }
        return true;
    }
}
