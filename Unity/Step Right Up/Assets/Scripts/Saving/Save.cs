using System;
using System.IO;
using UnityEngine;

public static class Save
{
    private static SaveData SaveDataInstance;

    private static string SaveFilePath => Application.persistentDataPath + Path.DirectorySeparatorChar + "Save.json";
    
    private static bool Initialized;
    
    public static bool InWindow => SaveDataInstance != null && SaveDataInstance.TimeSaveData != null ? SaveDataInstance.TimeSaveData.InWindow : false;
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

    public static void InitializeSaveData()
    {
        if (!Initialized)
        {
            Initialized = true;
            SaveAsFile();
        }
    }

    public static void SaveWindowEntry()
    {
        SaveDataInstance.TimeSaveData.InWindow = true;
        SaveAsFile();
    }

    public static void SaveWindowExit()
    {
        SaveDataInstance.TimeSaveData.InWindow = false;
        SaveAsFile();
    }

    public static void IncrementDaysLeft(int amount)
    {
        SaveDataInstance.TimeSaveData.CurrentDaysLeft += amount;
        SaveAsFile();
    }

    public static void IncrementDaysSucceeded(int amount)
    {
        SaveDataInstance.TimeSaveData.DaysSucceeded += amount;
        SaveAsFile();
    }

    public static void IncrementDaysFailed(int amount)
    {
        SaveDataInstance.TimeSaveData.DaysFailed += amount;
        SaveAsFile();
    }

    public static void IncrementDaysElapsed(int amount)
    {
        SaveDataInstance.TimeSaveData.DaysElapsed += amount;
        SaveAsFile();
    }

    public static MiniGameSaveData GetMiniGameSaveData()
    {
        return SaveDataInstance.MiniGameSaveData;
    }

    static void SaveAsFile()
    {
        string json = JsonUtility.ToJson(SaveDataInstance);
        File.WriteAllText(SaveFilePath, json);
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