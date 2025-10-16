[System.Serializable]
public class SaveData
{
    public bool AttemptedDailyChallenge = false;
    public TimeSaveData TimeSaveData;
    public MiniGameSaveData MiniGameSaveData;
        
    public SaveData()
    {
        TimeSaveData = new TimeSaveData();
        MiniGameSaveData = new MiniGameSaveData();
    }
}