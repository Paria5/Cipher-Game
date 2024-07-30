using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

class morseCode
{
    public static player player = new player();


    public char[] alphabets = new char[] {  'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
    private string[] morseLetters = new string[] { ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---", "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", "...", "-", "..-", "...-", ".--", "-..-", "-.--", "--.." };
    private Dictionary<char,string> alphabetToMorse;
    private Dictionary<string, char> morseToAlphabet;
    public morseCode()
    {
        alphabetToMorse = new Dictionary<char,string>();
        for (int i = 0; i < alphabets.Length; i++)
        {
            alphabetToMorse.Add(alphabets[i], morseLetters[i]);
        }
        morseToAlphabet= new Dictionary<string, char>();
        for (int i = 0; i < alphabets.Length; i++)
        {
            morseToAlphabet.Add(morseLetters[i], alphabets[i]);
        }
    }
    public void DisplayMorseCodeDictionary() //reference chatGPT 
        //had the problem of sorting the right line and used the righpading that chatgpt suggested
    {
        Console.WriteLine("+--------+---------+");
        Console.WriteLine("| Letter | Morse   |");
        Console.WriteLine("+--------+---------+");
        for (int i = 0; i < alphabets.Length; i++)
        {
            string letterColumn = alphabets[i].ToString().PadRight(8); 
            string morseColumn = morseLetters[i].PadRight(9); 
            Console.WriteLine($"|{letterColumn}|{morseColumn}|");
        }
        Console.WriteLine("+--------+---------+");
    }
    private string userMorseCodeEntry()
    {
        string enteredMorseCode = Console.ReadLine();
        return enteredMorseCode;
    }
    private bool morseCodeEntryCheck(string input, string refrence)
    {
        if (input == refrence)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void readAloudBeeps(string[] morseCode)
    {
        int dotDelay = 14;
        int speedDifference = 0;
        for (int j = 0; j < morseCode.Length; j++) {
            if (morseCode[j] != " ")
            {
                for (int i = 0; i < morseCode[j].Length; i++)
                {
                    switch (morseCode[j][i])
                    {
                        case '.':
                            Console.Beep(1000, 200); // Beep for dot
                            Thread.Sleep(dotDelay + speedDifference);
                            break;
                        case '-':
                            Console.Beep(1000, 400); // Beep for dash
                            Thread.Sleep(dotDelay + speedDifference);
                            break;
                    }
                }
                if (morseCodeEntryCheck(userMorseCodeEntry(), morseCode[j]))
                {
                    speedDifference -= 1;
                    player.GainHearts(1);
                    player.heartDisplay();

                }
                else
                {
                    Console.WriteLine("You've misheard it! You lost 1 heart. Be careful!");
                    speedDifference += 1;
                    player.LoseHearts(1);
                    player.heartDisplay();
                    j--;
                }
            }
            }
    }
    public string[] lettersToMorse(char[] cipheredTxt)
    {
        string[] lettersToMorse = new string[cipheredTxt.Length];
        for (int i = 0; (i < cipheredTxt.Length); i++)
        {
            lettersToMorse[i] = alphabetToMorse[cipheredTxt[i]];
        }
        return lettersToMorse;
    }
    public char[] wordsToLetters(string inputText)
    {
        char[] letters = new char[inputText.Length];
        letters = inputText.ToCharArray();
        for (int i = 0; i < letters.Length; i++)
        {
            if (char.IsLower(letters[i]))
            {
                letters[i] = char.ToUpper(letters[i]);
            }
        }
        return letters;
    }
}