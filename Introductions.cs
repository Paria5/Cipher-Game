using System;
using System.Linq;

class introductions
{
    public static player player=new player();
    public static game game = new game();
    public static morseCode morseCode=new morseCode();
    public void introduction()
    {
        Console.WriteLine("In the heart of World War II, you, the elite spy, embark on a perilous mission to infiltrate the German communication station." +
            " \nYour objective: extract crucial intel about imminent attacks, " +
            "transmit the altered plans to the British forces, and shift the tides of war." +
            " The covert operation unfolds in strategic steps.");
        player.name = ReadString("Please enter your name: ");
        Console.Clear();
        Console.WriteLine("Welcome sergeant " + player.name + "!");
        Console.WriteLine("In the game's outset, you begin with " + player.Heart + " hearts, and it's crucial to recognize that every 2 hearts is equivalent to a troop in the British army." +
            " The British forces commence the game with a total of 24 troops, encompassing both air and ground forces." +
            " These forces are further categorized, with each type having 12 troops, evenly distributed in four directions. " +
            "This distribution ensures that there are precisely three troops in each direction. " +
            "The German forces mirror this structure.");
        Console.WriteLine("Whenvever you are ready press any key to start the game.");
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
    public void partOne_intro()
    {
        Console.WriteLine("'Morse Code Mastery'\n");
        Console.ReadKey();
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

    public void partTwo_intro()
    {
        Console.WriteLine("'Deciphering Challenge'\n");
        Console.ReadKey();
        Console.WriteLine("Armed with the Morse translations, the next task is to uncover the corresponding letters. " +
            "A puzzle awaits as you match each Morse signal to its alphabetic counterpart. " +
            "The clock ticks, pushing you to swiftly decipher the encoded message. Time is critical in this step. If you don't guess the letters" +
            "in 10 seconds, the system thinks there is a problem in receiving the message and it will restart automatically. Also, you will " +
            "lose 1 heart wach time game restarts. Good luck sergeant!");
        Console.ReadKey();
        Console.Clear();
    }
    public void partTwo_end()
    {
        Console.WriteLine("Here is the Morse Code that you guessed:");
        string[] morseCodeFinal = morseCode.lettersToMorse(morseCode.wordsToLetters(game.secretCipheredMessage));
        if (game.secretCipheredMessage != null)
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
        Console.WriteLine("\nYou must know once you press Enter, the game starts.");
    }
    public void partThree_intro()
    {
        Console.WriteLine("'Hidden Key'\n");
        Console.ReadKey();
        Console.WriteLine("Looks Like this message has been ciphered! You must know how to decipher this message to access the information.");
        Console.ReadKey();
        Console.Write("You Look at that rookie's monitor again and you find some papers with some numbers and the name of Ceaser! That must be a clue!");
        Console.ReadKey();
        Console.WriteLine("Ceaser algorithm is a ciphering algorithm in which all the letters in the message are shifted with a shifting number.");
        Console.WriteLine("You realize that there are 26 possibilites to find the ciphered message, but you don't have time and also with each 3 wrong guesses for the shifted number you will lose a heart!");
        Console.ReadKey();
        Console.WriteLine("There must be a clue within the message itself about the shifting number. Try your best to find this number.");
        Console.ReadKey();
        Console.Clear();
    }
    public void partFour_intro()
    {
        Console.WriteLine("'Strategic Alteration'\n");
        Console.ReadKey();
        Console.WriteLine("Now comes the game-changer. Use the hidden number to alter the message, injecting misinformation into the enemy's plans.Your decisions will reshape the course of history.");
        Console.WriteLine("This is what was planned:");
        Console.WriteLine(game.secretMessageString);
        Console.WriteLine("Apparantly, Wolves stand for ground forces and Birds stand for air forces, and we " +
            "know the direction of the attacks.");
        Console.ReadKey();
        Console.Clear();
        Console.WriteLine("Before making a decision on how to change enemy's plans, you can take a look at the remaining troops of your own station as it might help you find a better decision of how to change the enemy's attacks so if their attack was successful your army has enough forces to defend itself.");
        Console.ReadKey();
        //show the dictionary of station_A
    }

    public void partFive_intro()
    {
        Console.WriteLine("'Ciphering The Altered Plan'\n");
        Console.ReadKey();
        Console.WriteLine("Now that you have modified the message, it is time for you to cipher this message back again with the same shifting number that you found before so that the enemy won't doubt anything.");
        Console.WriteLine("Enter the shifting number you found:");
        game.userInputShiftingNum();
        Console.WriteLine("Now, to save time, the system will provide you with the ciphered message with the chosen shifting number.");
        Console.WriteLine("Press any key to continue..");
        Console.ReadKey();
        Console.Clear();
    }
    public void partSix_intro()
    {
        Console.WriteLine("'Send It Out!'\n");
        Console.WriteLine("You are almost there! All you need to do is to transfer the modified message with Morse Code.");
        Console.ReadKey();
        Console.WriteLine("Oh no! The time is running out! you find an open system that converts your message automatically to Morse code.");
        Console.ReadKey();
        Console.WriteLine("Here is the message you must send out:\n" + game.cipheredAlteredmessage);
        Console.ReadKey();
        Console.WriteLine("Once you've put it in the system, the Morse code conversion happens.");
        Console.ReadKey();
        Console.WriteLine(string.Join(" ",morseCode.lettersToMorse(game.cipheredAlteredmessage.ToCharArray())));
        Console.ReadKey();
        Console.WriteLine("Press any key to send the message!");
        Console.ReadKey();
        Console.WriteLine("Message was sent successfully!");
    }
}