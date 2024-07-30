using System;


class player
{
    public string name;
    private int heart = 10;
    public HeartMonitor HeartMonitor = new HeartMonitor();
    public int Heart
    {
        get { return heart; }
        set
        {
            if (heart != value) 
            {
                heart = value;
                if (value < heart)
                {
                    HeartMonitor.OnHeartsLost();
                }
            }
        }
    }
    public void heartDisplay()
    {
        Console.WriteLine("Hearts: " + Heart);
    }
    public void LoseHearts(int heartsLost)
    {
        Heart -= heartsLost;
        if (Heart <= 0)
        {
            Console.WriteLine("Game Over! You ran out of hearts.");
            Environment.Exit(0); 
        }
    }
    public void GainHearts(int heartsGained)
    {
        Heart += heartsGained;
    }

}