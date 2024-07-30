using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

class ceasercipher
{
    morseCode morseCode=new morseCode();
    public char[] mostCommonEnglishLetters = new char[] { 'E', 'T', 'A', 'O', 'I', 'N', 'S', 'H', 'R', 'D', 'L', 'U', 'C', 'M', 'F', 'W', 'Y', 'P', 'B', 'G', 'V', 'K', 'X', 'J', 'Q', 'Z' };
    public int[] mostCommonLetterFreqs = new int[] { 13, 9, 8, 8, 7, 7, 6, 6, 6, 4, 4, 4, 3, 3, 3, 2, 2, 2, 1, 1, 1, 1, 1, 0, 0, 0 };
    private Dictionary<char, int> expectedFreq;

    public ceasercipher()
    {
        expectedFreq = new Dictionary<char, int>();
        for (int i = 0; i < mostCommonEnglishLetters.Length; i++)
        {
            expectedFreq.Add(mostCommonEnglishLetters[i], mostCommonLetterFreqs[i]);
        }
    }
    public string CeaserCipher(char[] originalMessage, int shiftNumber)
    {
        char[] cipheredText = new char[originalMessage.Length];
        for (int i = 0; i < originalMessage.Length; i++)
        {
            for (int j = 0; j < morseCode.alphabets.Length; j++)
            {
                if (originalMessage[i] == morseCode.alphabets[j] && originalMessage[i] != ' ')
                {
                    if (shiftNumber + j < morseCode.alphabets.Length)
                    {
                        cipheredText[i] = morseCode.alphabets[j + shiftNumber];
                    }
                    else
                    {
                        cipheredText[i] = morseCode.alphabets[j + shiftNumber - morseCode.alphabets.Length ];
                    }
                }
                if (originalMessage[i] == ' ')
                {
                    cipheredText[i] = ' ';
                }
            }
        }
        return new string(cipheredText);
    }
    public string CeaserDecipher(int shiftedNum, char[] cipheredText)
    {
        char[] decipheredText = new char[cipheredText.Length];
        for (int i = 0; i < cipheredText.Length; i++)
        {
            if (cipheredText[i] == ' ')
            {
                decipheredText[i] = ' ';
            }
            else
            {
                int j = Array.IndexOf(morseCode.alphabets, cipheredText[i]);

                if (j != -1)
                {
                    int newIndex = (j - shiftedNum + morseCode.alphabets.Length) % morseCode.alphabets.Length;
                    decipheredText[i] = morseCode.alphabets[newIndex];
                }
                else
                {
                    Console.WriteLine("The letter was not found in the alphabets!");
                }
            }
        }

        return new string(decipheredText);
    }
    public Dictionary<char, int> lettersAndFrequency(char[] cipheredMessage)
    {
        List<char> cipheredLetters = new List<char>(cipheredMessage);
        List<int> wordsFrequency = new List<int>();

        for (int i = cipheredLetters.Count - 1; i >= 0; i--)
        {
            if (char.IsWhiteSpace(cipheredLetters[i]))
            {
                cipheredLetters.RemoveAt(i);
            }
        }
        for (int i = 0; i < cipheredLetters.Count; i++)
        {
            wordsFrequency.Insert(i, 1);
            for (int j = i + 1; j < cipheredLetters.Count; j++)
            {
                if (cipheredLetters[j] == cipheredLetters[i])
                {
                    wordsFrequency[i]++;
                    cipheredLetters.RemoveAt(j);
                }
            }
        }
        Dictionary<char, int> lettersAndFrequency = new Dictionary<char, int>();
        for (int i = 0; i < cipheredLetters.Count; i++)
        {
            lettersAndFrequency.Add(cipheredLetters[i], wordsFrequency[i]);
        }

        return lettersAndFrequency;
    }

    public string findingTheDecipheredMessage(string cipheredMessage)
    {

        char[] estimatedDecipheredMessage;
        //ChatGPT for finding the most frequent letter
        char mostCommonLetter = lettersAndFrequency(cipheredMessage.ToCharArray()).ToList().OrderByDescending(pair => pair.Value).First().Key;
        for (int i = 0; i < morseCode.alphabets.Length; i++)
        {

            int estimatedShift = mostCommonLetter - mostCommonEnglishLetters[i];
            estimatedDecipheredMessage = CeaserDecipher(estimatedShift, cipheredMessage.ToArray()).ToCharArray();

            Dictionary<char, int> estimatedLetterFrequencies = lettersAndFrequency(estimatedDecipheredMessage);

            double similarityScore = CalculateSimilarityScore(estimatedLetterFrequencies);

            if (similarityScore > 0.7)
            {
                return new string(estimatedDecipheredMessage);
            }
        }
        return "No match found.";
    }

    private double CalculateSimilarityScore(Dictionary<char, int> letterFreq)
    {
        double dotProduct = 0;
        double magnitude1 = 0;
        double magnitude2 = 0;

        foreach (var pair in expectedFreq)
        {
            int value;
            letterFreq.TryGetValue(pair.Key, out value);

            dotProduct += pair.Value * value;
            magnitude1 += pair.Value * pair.Value;
            magnitude2 += value * value;
        }

        magnitude1 = Math.Sqrt(magnitude1);
        magnitude2 = Math.Sqrt(magnitude2);

        if (magnitude1 == 0 || magnitude2 == 0)
        {
            return 0.0;
        }

        return dotProduct / (magnitude1 * magnitude2);
    }
}