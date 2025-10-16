[System.Serializable]
public class TimeSaveData
{
    public bool InWindow = false;
    public int CurrentDaysLeft;
    public int DaysSucceeded = 0;
    public int DaysFailed = 0;
    public int DaysElapsed = 0;

    public TimeSaveData()
    {
        CurrentDaysLeft = Config.Data.ChallengeLengthInDays;
    }
}
