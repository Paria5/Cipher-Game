using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


class Station_A
{
    const int forcesNumber = 8;
    public Dictionary<string, int> troops;
    private readonly Random random = new Random();

    public Station_A(player player)
    {
        troops = new Dictionary<string, int>();
        string filePath = "D:\\Visual Studio Projects\\Final_Log\\bin\\Debug\\Directions.txt";
        try
        {
            StreamReader inputFile = new StreamReader(filePath);
            for (int i = 0; i < forcesNumber; i++)
            {
                string line = inputFile.ReadLine();

                if (line != null)
                {
                    string[] parts = line.Split('=');

                    if (parts.Length == 2)
                    {
                        troops.Add(parts[0], int.Parse(parts[1]));
                    }
                    else
                    {
                        Console.WriteLine("Invalid format!");
                    }
                }
                else
                {
                    Console.WriteLine("Not enough lines in the file.");
                    break;
                }
            }
            inputFile.Close();
        }

        catch (Exception ex)
        {
            Console.WriteLine("An error occurred:" + ex.Message);
        }
    }
}

