using System.IO;
using UnityEngine;

public static class Save
{
    // [System.Serializable]
    // private class CollectableData
    // {
    //     public int CollectableIndex;
    //     public int PlayerIndex;
    //
    //     public CollectableData(int collectableIndex, int playerIndex)
    //     {
    //         CollectableIndex = collectableIndex;
    //         PlayerIndex = playerIndex;
    //     }
    // }

    // [System.Serializable]
    // private class LevelData
    // {
    //     public int LevelChapter;
    //     public int LevelIndex;
    //     public bool Completed;
    //
    //     public List<CollectableData> FlyingCollectableList;
    //     public CollectableData[] FlyingCollectableArray => FlyingCollectableList.ToArray();
    //
    //     public List<CollectableData> ShootableCollectableList;
    //     public CollectableData[] ShootableCollectableArray => ShootableCollectableList.ToArray();
    //
    //     public LevelData(LevelType chapter, int levelIndex)
    //     {
    //         LevelChapter = (int)chapter;
    //         LevelIndex = levelIndex;
    //         FlyingCollectableList = new List<CollectableData>();
    //         ShootableCollectableList = new List<CollectableData>();
    //     }
    // }
    //
    // [System.Serializable]
    // private class ChapterData
    // {
    //     public bool NarrativeTutorialCompleted;
    // }

    [System.Serializable]
    private class SaveData
    {
        // public bool MechanicTutorialCompleted;
        // public List<LevelData> LevelData;
        // public List<ChapterData> ChapterData;
        public bool DailyChallengeWindowOpen = false;
        public SaveData()
        {
            // LevelData = new List<LevelData>();
            // ChapterData = new List<ChapterData>();
        }
    }
    
    private static SaveData SaveDataInstance;

    private static string SaveFilePath => Application.persistentDataPath + Path.DirectorySeparatorChar + "Save.json";

    // private static Dictionary<LevelType, Dictionary<int, LevelData>> LevelDataDictionary;
    // private static Dictionary<LevelType, ChapterData> ChapterDataDictionary;

    private static bool Initialized;

    // public static bool IsMechanicsTutorialCompleted => SaveDataInstance != null ? SaveDataInstance.MechanicTutorialCompleted : false;

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
                SaveDataInstance = new SaveData();
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

    // private static void InitializeChapterData()
    // {
    //     ChapterDataDictionary = new Dictionary<LevelType, ChapterData>();
    //     ChapterDataDictionary.Add(LevelType.Moth, new ChapterData());
    //     ChapterDataDictionary.Add(LevelType.Teen, new ChapterData());
    //     ChapterDataDictionary.Add(LevelType.Spaceship, new ChapterData());
    //     ChapterDataDictionary.Add(LevelType.Icarus, new ChapterData());
    // }
    //
    // private static void InitializeLevelData()
    // {
    //     LevelDataDictionary = new Dictionary<LevelType, Dictionary<int, LevelData>>();
    //     foreach(LevelData levelData in SaveDataInstance.LevelData)
    //     {
    //         LevelType chapter = (LevelType)levelData.LevelChapter;
    //         if(!LevelDataDictionary.ContainsKey(chapter))
    //         {
    //             LevelDataDictionary.Add(chapter, new Dictionary<int, LevelData>());
    //         }
    //
    //         if (!LevelDataDictionary[chapter].ContainsKey(levelData.LevelIndex))
    //         {
    //             LevelDataDictionary[chapter].Add(levelData.LevelIndex, levelData);
    //         }
    //     }
    // }
    //
    // public static void SaveCollectables(LevelType chapter, int levelIndex, Dictionary<int, int> flyingCollectables, Dictionary<int, int> shootableCollectables)
    // {
    //     LevelData levelData = TryGetLevelData(chapter, levelIndex);
    //     if (levelData != null)
    //     {
    //         foreach (KeyValuePair<int, int> kvp in flyingCollectables)
    //         {
    //             if(!levelData.FlyingCollectableList.Any(c => c.CollectableIndex == kvp.Key))
    //                 levelData.FlyingCollectableList.Add(new CollectableData(kvp.Key, kvp.Value));
    //         }
    //
    //         foreach (KeyValuePair<int, int> kvp in shootableCollectables)
    //         {
    //             if(!levelData.ShootableCollectableList.Any(c => c.CollectableIndex == kvp.Key))
    //                 levelData.ShootableCollectableList.Add(new CollectableData(kvp.Key, kvp.Value));
    //         }
    //         
    //         SaveAsFile();
    //     }
    // }
    //
    // public static bool TryGetCollectableData(LevelType chapter, int levelIndex, out Dictionary<int, int> flyingCollectables, out Dictionary<int, int> shootableCollectables)
    // {
    //     flyingCollectables = new Dictionary<int, int>();
    //     shootableCollectables = new Dictionary<int, int>();
    //     LevelData levelData = TryGetLevelData(chapter, levelIndex);
    //     if (levelData != null)
    //     {
    //         if (levelData.FlyingCollectableList != null)
    //             flyingCollectables = levelData.FlyingCollectableList.ToDictionary(fc => fc.CollectableIndex, fc => fc.PlayerIndex);
    //         
    //         if(levelData.ShootableCollectableList != null)
    //             shootableCollectables = levelData.ShootableCollectableList.ToDictionary(sc => sc.CollectableIndex, sc => sc.PlayerIndex);
    //         return true;
    //     }
    //     return false;
    // }
    //
    // public static bool IsLevelCompleted(LevelType chapter, int levelIndex)
    // {
    //     LevelData levelData = TryGetLevelData(chapter, levelIndex);
    //     return levelData.Completed;
    // }
    //
    // public static bool IsNarrativeTutorialCompleted(LevelType chapter)
    // {
    //     if (ChapterDataDictionary.ContainsKey(chapter))
    //     {
    //         return ChapterDataDictionary[chapter].NarrativeTutorialCompleted;
    //     }
    //
    //     return false;
    // }
    //
    // public static void TryCompleteMechanicsTutorial()
    // {
    //     if (!SaveDataInstance.MechanicTutorialCompleted)
    //         SaveDataInstance.MechanicTutorialCompleted = true;
    // }
    //
    // public static void CompleteNarrativeTutorial(LevelType chapter)
    // {
    //     if (ChapterDataDictionary.ContainsKey(chapter))
    //     {
    //         ChapterDataDictionary[chapter].NarrativeTutorialCompleted = true;
    //     }
    // }
    //
    // public static void CompleteLevel(LevelType chapter, int levelIndex)
    // {
    //     LevelData levelData = TryGetLevelData(chapter, levelIndex);
    //     levelData.Completed = true;
    //     SaveAsFile();
    // }
    //
    // public static void CompleteLevel()
    // {
    //     if (SceneController.Instance.CurrentChapter == null)
    //         return;
    //     CompleteLevel(SceneController.Instance.CurrentChapter.LevelType, SceneController.Instance.CurrentLevelIndex - 1);
    //     //GameController.Instance.LightController.ToggleConstellationLight(SceneController.Instance.CurrentChapter.LevelType, SceneController.Instance.CurrentLevelIndex - 1, true);
    // }
    //
    // private static LevelData TryGetLevelData(LevelType chapter, int levelIndex)
    // {
    //     InitializeSaveData();
    //     if(!LevelDataDictionary.ContainsKey(chapter))
    //     {
    //         LevelDataDictionary.Add(chapter, new Dictionary<int, LevelData>());
    //     }
    //
    //     if (!LevelDataDictionary[chapter].ContainsKey(levelIndex))
    //     {
    //         LevelData levelData = new LevelData(chapter, levelIndex);
    //         SaveDataInstance.LevelData.Add(levelData);
    //         LevelDataDictionary[chapter].Add(levelData.LevelIndex, levelData);
    //         SaveAsFile();
    //     }
    //     return LevelDataDictionary[chapter][levelIndex];
    // }

    public static void SaveAsFile()
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