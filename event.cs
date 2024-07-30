using System;

public class HeartMonitor
{
    public event EventHandler HeartsLost;

    public void OnHeartsLost()
    {
        HeartsLost?.Invoke(this, EventArgs.Empty);
    }
    public void SubscribeToHeartsLostEvent(EventHandler handler)
    {
        HeartsLost += handler;
    }
    public void UnsubscribeFromHeartsLostEvent(EventHandler handler)
    {
        HeartsLost -= handler;
    }
}
