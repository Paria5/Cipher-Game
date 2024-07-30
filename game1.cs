using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

class game
{
    public static player player = new player();
    public static morseCode morseCode = new morseCode();
    public static ceasercipher ceasercipher = new ceasercipher();
    public static introductions introductions = new introductions();
    public static HashSet<char> usedLetters = new HashSet<char>();
    private readonly Station_A StationA;
    public static string secretMessageString;
    public static string secretCipheredMessage;
    public static string cipheredAlteredmessage;
    private static int shiftNumber;
    private bool timeout = false;
    private bool restart = true;
    public static string alteredPlan;


    public void start()
    {
        startGame();
        partOne_receivingMorseCode();
        partTwo_MorseDecodingGame();
        partThree_decipherMessage();
        partFour_changeTheMessage();
        partFive_cipherBack();
        partSix_transmitMorseCode();
        EndOfGame();
    }
    private void startGame()
    {
        DisplayGameName("Silent Saboteur");
        introductions.introduction();
    }
    public void partOne_receivingMorseCode()
    {
        introductions.partOne_intro();
        secretMessageString = plannedAttack();
        shiftNumber = secretMessageString.Length;
        secretCipheredMessage = ceasercipher.CeaserCipher(morseCode.wordsToLetters(secretMessageString), shiftNumber);
        //morseCode.readAloudBeeps(morseCode.lettersToMorse(morseCode.wordsToLetters(secretCipheredMessage)));
        Console.WriteLine("Well done sergeant! You survived this step. You have " + morseCode.player.Heart + " hearts left.");
        Console.ReadKey();
        Console.Clear();
    }
    private void partTwo_MorseDecodingGame()
    {
        introductions.partTwo_intro();
        morseDeCoding_options();
        introductions.partTwo_end();
        repetition(morseDecoding_guessTheletters());
        Console.ReadKey();
        Console.Clear();

    }
    public void partThree_decipherMessage()
    {
        introductions.partThree_intro();
        decipherMessage_guessTheWord();
    }
    public void partFour_changeTheMessage()
    {
        introductions.partFour_intro();
        userAlteration();
        Console.ReadKey();
        Console.Clear();
    }
    public void partFive_cipherBack()
    {
        introductions.partFive_intro();
        cipheringTheModifiedMessage();
    }
    public void partSix_transmitMorseCode()
    {
        introductions.partSix_intro();
    }
    public void EndOfGame()
    {
        Console.WriteLine("Congradulations! You have saved your station from an attack.");
        player.heartDisplay();
        Environment.Exit(0);
        //station display
    }


    public void cipheringTheModifiedMessage()
    {
        Console.WriteLine("This is your modified message:");
        Console.WriteLine(alteredPlan);
        Console.ReadKey();
        Console.WriteLine("And this is the ciphered version of your modified message.");
        cipheredAlteredmessage = ceasercipher.CeaserCipher(alteredPlan.ToUpper().ToCharArray(), shiftNumber);
        Console.WriteLine(cipheredAlteredmessage);
        Console.ReadKey();
        Console.WriteLine("Now it is time for you to send this message!\nPress any key to continue..");
        Console.ReadKey();
        Console.Clear();

    }
    public void userInputShiftingNum()
    {
        bool validguess = false;
        while (!validguess)
        {
            string userInput = Console.ReadLine();
            if (int.TryParse(userInput, out int shiftnum) && shiftnum == shiftNumber)
            {
                if (shiftnum == shiftNumber)
                {
                    Console.WriteLine("Well done.");
                    Console.ReadKey();
                    Console.Clear();
                    validguess = true;
                }
                else
                {
                    Console.WriteLine("You must have remembered it! You've lost a heart, try again!");
                    player.LoseHearts(1);
                    player.heartDisplay();
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }
    }
    public string userAlteration()
    {
        string chosenDirection = choosingDirection();
        string chosenForce = choosingForceType();
        bool isChangeCorrect;
        do
        {
            Console.WriteLine("Is this the change that you wished for?");
            Console.WriteLine(chosenDirection + chosenForce);
            Console.WriteLine("(1) Yes               (2) No");
            string userResponse = Console.ReadLine();

            if (userResponse == "1")
            {
                isChangeCorrect = true;
                Console.WriteLine("Great! Press any key to go to the next level..");
            }
            else if (userResponse == "2")
            {
                Console.WriteLine("C'mon! we don't have time!! Choose wisely!");
                chosenDirection = choosingDirection();
                chosenForce = choosingForceType();
                isChangeCorrect = false;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter '1' for Yes or '2' for No.");
                isChangeCorrect = false;
            }
        } while (!isChangeCorrect);
        alteredPlan = chosenDirection + chosenForce;
        return (alteredPlan);
    }
    public string choosingDirection()
    {
        Console.WriteLine("Use one of the following directions to alter the plan:");
        Console.WriteLine("(1) North             (2) South");
        Console.WriteLine("(3) East              (4) West");
        int inputInt;
        bool validInput = false;
        do
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out inputInt) && inputInt >= 1 && inputInt <= 4)
            {
                validInput = true;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
                validInput = false;
            }
        } while (!validInput);

        return GetDirection(inputInt);
    }
    public string choosingForceType()
    {
        Console.WriteLine("Now choose the type of force you want to change the plan to:");
        Console.WriteLine("(1) Air force         (2) Ground force");
        int inputInt;
        bool validInput = false;
        do
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out inputInt) && inputInt >= 1 && inputInt <= 2)
            {
                validInput = true;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 1 or 2.");
                validInput = false;
            }
        } while (!validInput);

        return GetForceType(inputInt);
    }
    public string GetDirection(int choice)
    {
        switch (choice)
        {
            case 1:
                return "Northern";
            case 2:
                return "Southern";
            case 3:
                return "Eastern";
            case 4:
                return "Western";
            default:
                return "Unknown";
        }
    }
    public string GetForceType(int choice)
    {
        switch (choice)
        {
            case 1:
                return "Birds";
            case 2:
                return "Wolves";
            default:
                return "Unknown";
        }
    }
    public void decipherMessage_guessTheWord()
    {
        //Assuming the computer itself doesn't know the secretMessage. I implemented a deciphering method by computer on the basis of frequency analysis
        string userGuess;
        Console.WriteLine("This is the ciphered message: ");
        Console.WriteLine(secretCipheredMessage);
        Console.WriteLine("Start guesssing the shift number to see if we can find that secret message! ");
        bool guessedRight = false;
        while (!guessedRight)
        {
            string guessedNum = Console.ReadLine();
            if (!string.IsNullOrEmpty(guessedNum) && int.TryParse(guessedNum, out int validInt))
            {
                bool correctGuess = false;
                for (int attempt = 0; attempt < 3; attempt++)
                {
                    userGuess = ceasercipher.CeaserDecipher(int.Parse(guessedNum), secretCipheredMessage.ToCharArray());
                    Console.WriteLine(userGuess);
                    string computerDeciphered = ceasercipher.findingTheDecipheredMessage(secretCipheredMessage).ToUpper();
                    if (userGuess == computerDeciphered)
                    {
                        Console.WriteLine("Looks like you've found it!");
                        Console.ReadKey();
                        Console.WriteLine("Press any key to continue..");
                        correctGuess = true;
                        guessedRight = true;
                        break;
                    }
                    else if (attempt == 2)
                    {
                        Console.WriteLine("Wrong guess! No guesses left for this round of game.");
                        Console.WriteLine("You've lost a heart.");
                        player.LoseHearts(1);
                        player.heartDisplay();
                    }
                    else
                    {
                        Console.WriteLine("Wrong guess! " + (2 - attempt) + " guesses left to lose a heart");
                        guessedNum = Console.ReadLine();
                    }
                }
            }
            else
            {
                Console.WriteLine("Wrong input. You lost 1 heart. Pay more attention!");
                player.LoseHearts(1);
                player.heartDisplay();
            }
        }
        Console.ReadKey();
        Console.Clear();
    }
    public void repetition(bool restartingStatus)
    {
        while (restartingStatus)
        {
            restartingStatus = morseDecoding_guessTheletters();
        }
    }
    public bool morseDecoding_guessTheletters()
    {
        timeout = false;
        usedLetters.Clear();
        Console.ReadKey();
        //ChatGPT
        //my question: I want to set a time constraint for a player for 10 sec, how can I do that in c#?
        //I took the parts I needed from the code it provided
        var timerTask = Task.Run(() =>
        {
            Task.Delay(20000).Wait();
            timeout = true;
        });

        char[] secretMessage = morseCode.wordsToLetters(secretCipheredMessage);
        char[] guessedSpaces = new char[secretMessage.Length];
        initialDisplay(guessedSpaces);

        while (guessedSpaces.Contains('-') && !timeout)
        {
            string guessedInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(guessedInput) && guessedInput.Length == 1 && !int.TryParse(guessedInput, out int result))
            {
                char guessedLetter = guessedInput[0];
                guessedLetter = char.ToUpper(guessedLetter);

                //asked ChatGPT how can I state that if the guessed letter was used before 
                //introduced me to the concept of HashSet that stores the guessed letters, I declared the class at the top
                if (usedLetters.Contains(guessedLetter))
                {
                    Console.WriteLine("You've already guessed this letter. Try a different one.");
                    continue;
                }
                usedLetters.Add(guessedLetter);

                if (letterIn(guessedLetter, secretMessage))
                {
                    updateTheLetter(guessedLetter, secretMessage, guessedSpaces);
                    for (int i = 0; i < guessedSpaces.Length; i++)
                    {
                        Console.Write(guessedSpaces[i]);
                    }

                }
                else
                {
                    Console.Write("Wrong input. You lost 1 heart. Pay more attention!");
                    player.LoseHearts(1);
                    player.heartDisplay();
                }
            }
            else
            {
                Console.Write("Wrong input. You lost 1 heart. Pay more attention!");
                player.LoseHearts(1);
                player.heartDisplay();
            }
        }
        if (timeout)
        {
            Console.WriteLine("\nTime's up! You have lost a heart! Restarting from the beginning.");
            player.LoseHearts(1);
            player.heartDisplay();
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine(string.Join(" ", morseCode.lettersToMorse(morseCode.wordsToLetters(secretCipheredMessage))));
            return restart = true;
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Well done! You got through this level with " + morseCode.player.Heart + " hearts.");
            Console.WriteLine("This is the message you revealed:");
            Console.WriteLine(secretCipheredMessage);
            Console.WriteLine("Press any key to continue..");
            return restart = false;
        }
    }
    public void initialDisplay(char[] guessedWords)
    {
        for (int i = 0; i < guessedWords.Length; i++)
        {
            guessedWords[i] = '-';
        }
    }
    public bool letterIn(char letter, char[] Message)
    {
        return Message.Contains(letter);
    }
    public void updateTheLetter(char inputLetter, char[] message, char[] guessedSpaces)
    {
        for (int i = 0; i < message.Length; i++)
        {
            if (message[i] == inputLetter)
            {
                guessedSpaces[i] = inputLetter;
            }
        }
    }

    public void morseDeCoding_options()
    {
        bool validInput = false;
        Console.WriteLine("Choose one of the following options: ");
        Console.WriteLine("(1) I know the Morse Code.      (2) I don't know Morse Code. Let's look for a clue.");
        while (!validInput)
        {
            string userInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(userInput) && userInput.Length == 1 && int.TryParse(userInput, out int options))
            {
                switch (options)
                {
                    case 1:

                        Console.Clear();
                        Console.WriteLine("You are a skilled sergeant, well done! You will gain 2 hearts because of your skill of decoding morse code.");
                        player.GainHearts(2);
                        validInput = true;
                        break;

                    case 2:

                        Console.Clear();
                        Console.WriteLine("You look for a clue in the room, you notice a guide that has some dots and dashes on the screen of one of the monitors!" +
                            "It might be the Morse Code that one of the rookies of the German's army has set up to help him decode the Morse code.");
                        Console.WriteLine("This is the table you find:");
                        morseCode.DisplayMorseCodeDictionary();
                        validInput = true;
                        break;

                    default:
                        Console.WriteLine("Invalid input. Please enter 1 or 2.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 1 or 2.");
                validInput = false;
            }
        }
    }
    public string plannedAttack()
    {
        Station_A station_A = new Station_A(player);
        Dictionary<string, int> troops = station_A.troops;
        int randomIndex = new Random().Next(troops.Count);
        string plannnedAttack = troops.ElementAt(randomIndex).Key;
        return plannnedAttack;
    }

    private void DisplayGameName(string gameName)
    {
        int consoleWidth = Console.WindowWidth;

        int boxWidth = gameName.Length + 4;
        int boxHeight = 3;

        int leftMargin = (consoleWidth - boxWidth) / 2;
        int topMargin = 0;

        Console.SetCursorPosition(leftMargin, topMargin);
        Console.WriteLine(new string('-', boxWidth));


        Console.SetCursorPosition(leftMargin, topMargin + 1);
        Console.WriteLine("| " + gameName + " |");

        Console.SetCursorPosition(leftMargin, topMargin + boxHeight - 1);
        Console.WriteLine(new string('-', boxWidth));

        Console.WriteLine("Press any key to start..");
        Console.ReadKey();
        Console.Clear();
    }
}