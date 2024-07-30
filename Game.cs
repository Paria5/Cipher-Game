using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

class game
{
    public static player player=new player();
    public static morseCode morseCode = new morseCode();
    private string secretMessageString;
    private int shiftNumber;
    private bool timeout = false;
    private bool restart = false;


    public void start()
    {
        startGame();
        transmitMorseCode();
        cipheredText();

    }
    private void cipheredText()
    {
        partTwo_intro();
        morseDecoding_options();
        repetition(morseDecoding_guessTheletters());
    }

    public void repetition(bool restartingStatus)
    {
        while (restartingStatus==true)
        {
            morseDecoding_guessTheletters();
        }
    }
    public bool morseDecoding_guessTheletters()
    {
        Console.ReadKey();
        var timerTask = Task.Run(() =>
        {
            Task.Delay(30000).Wait(); // Wait for 30 seconds
            timeout = true;
        });

        char[] secretMessage = morseCode.wordsToLetters(secretMessageString);
        char[] guessedSpaces = new char[secretMessage.Length];
        initialDisplay(guessedSpaces);

        while (guessedSpaces.Contains('-') && !timeout)
        {
            string guessedInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(guessedInput) && guessedInput.Length == 1 && !int.TryParse(guessedInput, out int result))
            {
                char guessedLetter = guessedInput[0];
                guessedLetter=char.ToUpper(guessedLetter);

                if (letterIn(guessedLetter, secretMessage))
                {
                    updateTheLetter(guessedLetter, secretMessage, guessedSpaces);
                    for(int i = 0; i < guessedSpaces.Length; i++)
                    {
                        Console.Write(guessedSpaces[i]);
                    }
                    Console.WriteLine("\n");
                }
                else
                {
                    Console.WriteLine("Wrong input. You lost 1 heart. Pay more attention!");
                    morseCode.player.heart -= 1;
                }
            }
            else
            {
                Console.WriteLine("Wrong input. You lost 1 heart. Pay more attention!");
                morseCode.player.heart -= 1;
            }
        }
        if (timeout)
        {
            Console.WriteLine("\nTime's up! Restarting from the beginning.");
            return restart = true;
        }
        else
        {
            Console.WriteLine("\nwell done!");
            return restart = false;
        }

    }
    public void updateTheLetter(char inputLetter,char[] message, char[] guessedSpaces)
    {
        for(int i = 0; i < message.Length; i++)
        {
            if (message[i] == inputLetter)
            {
                guessedSpaces[i] = inputLetter;
            }
        }
    }
   public bool letterIn(char letter,char[] Message)
    {
        return Message.Contains(letter);
    }
   public void initialDisplay(char[] guessedWords)
    {
        for (int i = 0; i < guessedWords.Length; i++)
        {
            guessedWords[i]='-';
        }
    }
    public void morseDecoding_options()
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
                        Console.WriteLine("You are a skilled sergeant, well done! You will gain 10 hearts because of your skill of decoding morse code.");
                        morseCode.player.heart = +20;
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
        Console.WriteLine("You must know once you press Enter, the game starts.");
        Console.WriteLine("Here is the Morse Code that you guessed:");
        string[] morseCodeFinal = morseCode.lettersToMorse(morseCode.wordsToLetters(secretMessageString));
        if (secretMessageString != null)
        {
            for (int i = 0; i < morseCodeFinal.Length; i++)
            {
                Console.Write(morseCodeFinal[i] + " ");
            }
        }
        else
        {
            Console.WriteLine("Secret Message is null!");
        }
        Console.WriteLine("\n");
    }
    private void partTwo_intro()
    {
        Console.WriteLine("Deciphering Challenge:\n");
        Console.WriteLine("Armed with the Morse translations, the next task is to uncover the corresponding letters. " +
            "A puzzle awaits as you match each Morse signal to its alphabetic counterpart. " +
            "The clock ticks, pushing you to swiftly decipher the encoded message. Time is critical in this step. If you don't guess the letters" +
            "in 10 seconds, the system thinks there is a problem in receiving the message and it will restart automatically. Also, you will " +
            "lose 1 heart wach time game restarts. Good luck sergeant!");
        Console.ReadKey();
        Console.Clear();
    }
    public void transmitMorseCode()
    {
        partOne_intro();
        secretMessageString = plannedAttack();
        shiftNumber = secretMessageString.Length;
        morseCode.readAloudBeeps(morseCode.lettersToMorse(morseCode.wordsToLetters(secretMessageString)));
        Console.WriteLine("Well done sergeant! You survived this step. You have "+morseCode.player.heart+ " hearts left.");
        Console.ReadKey();
        Console.Clear();
    }
 
    public string plannedAttack()
    {
        Station_A station_A = new Station_A();
        Dictionary<string, int> troops = station_A.troop;
        int randomIndex = new Random().Next(troops.Count);
        string plannnedAttack = troops.ElementAt(randomIndex).Key;
        return plannnedAttack;
    }
    private void partOne_intro()
    {
        Console.WriteLine("Morse Code Mastery:\n");
        Console.WriteLine("As you slip into the communication hub, you encounter a series of Morse code transmissions." +
            "The catch? The speed varies, challenging your decoding skills." +
            "Listen keenly, transcribe the dots and dashes, and gather the encrypted information.");
        Console.WriteLine("Successfully decoding one Morse code will earn you an additional heart, " +
            "and each accurate guess grants extra hearts. However, missing a code results in the loss of one heart," +
            " and the decoding speed will decrease");
        Console.WriteLine("When you're prepared, hit Enter to begin receiving the Morse codes.");
        Console.ReadKey();
        Console.Clear();
    }
    private void startGame()
    {
        DisplayGameName("Silent Saboteur");
        Console.Write("\n\n");
        introduction();
    }
    private void introduction()
    {
        Console.WriteLine("In the heart of World War II, you, the elite spy, embark on a perilous mission to infiltrate the German communication station." +
            " \nYour objective: extract crucial intel about imminent attacks, " +
            "transmit the altered plans to the British forces, and shift the tides of war." +
            " The covert operation unfolds in strategic steps.");
        player.name = ReadString("Please enter your name: ");
        Console.Clear();
        Console.WriteLine("Welcome sergeant "+player.name+"!");
        Console.WriteLine("In the game's outset, you begin with 10 hearts, and it's crucial to recognize that each heart is equivalent in value to two troops in the British army." +
            " The British forces commence the game with a total of 24 troops, encompassing both air and ground forces." +
            " These forces are further categorized, with each type having 12 troops, evenly distributed in four directions. " +
            "This distribution ensures that there are precisely three troops in each direction. " +
            "The German forces mirror this structure.");
        Console.WriteLine("Whenvever you are ready press Enter to start the game.");
        Console.ReadKey();
        Console.Clear();  
    }
    private string ReadString(string input)
    {
        string enteredName;
        do
        {
            Console.Write(input);
            enteredName = Console.ReadLine();
        } while (enteredName == "");
        return enteredName;
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
        Console.WriteLine("| "+gameName+" |");
  
        Console.SetCursorPosition(leftMargin, topMargin+boxHeight-1);
        Console.WriteLine(new string('-', boxWidth));
    }
}