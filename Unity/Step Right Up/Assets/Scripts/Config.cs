using System.IO;
using UnityEngine;

[System.Serializable]
public class ConfigData
{
    public int WindowStartHour = 0;
    public int WindowStartMinute = 0;
    public int WindowStartSecond = 0;
    public int WindowEndHour = 4;
    public int WindowEndMinute = 0;
    public int WindowEndSecond = 0;
    // public float InsetHeightPercentage = 2f;
    // public float ResetMovementThreshold = 0.01f;
    // public ColorData InsetColor = new ColorData();
    // public OneEuroFilterData FilterData = new OneEuroFilterData();
}

public static class Config
{
    private static string ConfigFilePath => (Application.isEditor ? Application.persistentDataPath : Application.dataPath) + Path.DirectorySeparatorChar + "Config.json";

    public static ConfigData Data;
    
    static Config()
    {
        if (File.Exists(ConfigFilePath))
        {
            using (StreamReader r = new StreamReader(ConfigFilePath))
            {
                string json = r.ReadToEnd();
                Data = JsonUtility.FromJson(json, typeof(ConfigData)) as ConfigData;
            }

            if (Data == null)
            {
                Data = new ConfigData();
                SaveAsFile();
            }
            else
            {
                SaveAsFile();
            }
        }
        else
        {
            Data = new ConfigData();
            SaveAsFile();
        }
        
    }
    
    private static void SaveAsFile()
    {
        string json = JsonUtility.ToJson(Data);
        System.IO.File.WriteAllText(ConfigFilePath, json);
    }
}