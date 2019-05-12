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


    HashSet<string> enteredWords = new HashSet<string>();

    public TextAsset wordsFile;
    HashSet<string> allEnglishWords = new HashSet<string>();

    void Start()
    {
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

        // initialize set of english words
        string[] words = wordsFile.text.Split(new[] { '\r', '\n' });
        Debug.Log(words[0]);
        Debug.Log(words[0].Length);
        foreach (string word in words)
        {
            allEnglishWords.Add(word);
        }
        foreach (char letter in alphabet)
        {
            allEnglishWords.Remove(letter.ToString());
        }
        allEnglishWords.Add("a");
        allEnglishWords.Add("i");
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
        if (!enteredWords.Contains(word) && allEnglishWords.Contains(word))
        {
            GameData.score += calcScore(word);
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
