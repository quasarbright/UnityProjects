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

    public Text scoreText;

    int score = 0;

    HashSet<string> enteredWords = new HashSet<string>();

    // Start is called before the first frame update
    void Start()
    {
        // randomize characters on startup
        System.Random rand = new System.Random();
        int count = 0;
        // initialize usable letters randomly
        foreach (Text text in texts)
        {
            char letter;
            // ensure 2 vowels
            if (count < 2)
            {
                int index = rand.Next(0, 5);
                letter = vowels[index];
            }
            else
            {
                int index = rand.Next(0, 21);
                letter = consonants[index];
            }
            usableLetters.Add(letter);
            count++;
        }
        // shuffle
        usableLetters.Sort((x, y) => rand.Next(-1,2));
        for (int i = 0; i < texts.Count; i++)
        {
            Text text = texts[i];
            char letter = usableLetters[i];
            text.text = letter.ToString();
        }

        // add input change listener
        input.onValueChanged.AddListener(delegate { OnChange(); });
        input.onEndEdit.AddListener(delegate { OnEnd(); });
        input.Select();
        input.ActivateInputField();

        scoreText.text = score.ToString();
    }

    void OnChange()
    {
        string newWord = input.text;
        if (!IsValid(newWord))
        {
            input.text = word;
        }
        else
        {
            input.text = newWord;
            word = newWord;
        }
    }

    void OnEnd()
    {
        if (!enteredWords.Contains(word))
        {
            score += calcScore(word);
            scoreText.text = score.ToString();
            enteredWords.Add(word);
        }
        input.text = "";
        input.Select();
        input.ActivateInputField();
    }

    static int calcScore(string word)
    {
        return word.Length * 100;
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
