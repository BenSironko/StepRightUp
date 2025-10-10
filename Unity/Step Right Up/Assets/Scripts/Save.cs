using System;
using System.IO;
using UnityEngine;

public static class Save
{
    [System.Serializable]
    private class SaveData
    {
        public bool InWindow = false;
        public bool AttemptedDailyChallenge = false;
        public string LastWindowOpen = "";
        public int CurrentChallengeLengthInDays;
        public int DaysSucceeded = 0;
        
        public SaveData()
        {
            CurrentChallengeLengthInDays = Config.Data.ChallengeLengthInDays;
        }
    }
    
    private static SaveData SaveDataInstance;

    private static string SaveFilePath => Application.persistentDataPath + Path.DirectorySeparatorChar + "Save.json";
    
    private static bool Initialized;
    
    public static bool InWindow => SaveDataInstance != null ? SaveDataInstance.InWindow : false;
    public static bool AttemptedDailyChallenge
    {
        get => SaveDataInstance != null ? SaveDataInstance.AttemptedDailyChallenge : false; 
        set
        {
            if (SaveDataInstance != null)
            {
                SaveDataInstance.AttemptedDailyChallenge = value;
            }
        }
    }
    
    static Save()
    {
        if (File.Exists(SaveFilePath))
        {
            using (StreamReader r = new StreamReader(SaveFilePath))
            {
                string json = r.ReadToEnd();
                SaveDataInstance = JsonUtility.FromJson(json, typeof(SaveData)) as SaveData;
            }

            if (SaveDataInstance == null)
            {
                SaveDataInstance = new SaveData();
            }
        }
        else
        {
            SaveDataInstance = new SaveData();
        }
    }

    private static void InitializeSaveData()
    {
        if (!Initialized)
        {
            Initialized = true;
        }
    }

    public static void SaveWindowEntry()
    {
        SaveDataInstance.InWindow = true;
        SaveDataInstance.LastWindowOpen = DateTime.UtcNow.ToString();
        SaveAsFile();
    }

    public static void SaveWindowExit()
    {
        SaveDataInstance.InWindow = false;
        SaveAsFile();
    }

    static void SaveAsFile()
    {
        string json = JsonUtility.ToJson(SaveDataInstance);
        System.IO.File.WriteAllText(SaveFilePath, json);
    }

    public static void DeleteSaveFile()
    {
        if (File.Exists(SaveFilePath))
        {
            File.Delete(SaveFilePath);
        }
        SaveDataInstance = new SaveData();
        Initialized = false;
        InitializeSaveData();
    }
}