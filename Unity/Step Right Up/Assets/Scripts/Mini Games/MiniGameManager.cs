using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField]
    private List<MiniGameConfig> m_MiniGameConfigs;
    private List<MiniGameConfig> MiniGameConfigs => m_MiniGameConfigs;
    
    private Dictionary<MiniGameType, MiniGameConfig> m_MiniGameConfigsByType;

    private Dictionary<MiniGameType, MiniGameConfig> MiniGameConfigsByType
    {
        get
        {
            if (m_MiniGameConfigsByType == null)
            {
                m_MiniGameConfigsByType = MiniGameConfigs.ToDictionary(mg => mg.Type, mg => mg);
            }
            return m_MiniGameConfigsByType;
        }
    }
    
    private List<MiniGameType> MiniGameBag;
    private List<MiniGameType> UnlockedMiniGames;
    public List<MiniGameType> PracticeMiniGames { get; private set; }
    private MiniGameType NextMiniGame;
    private MiniGameType LastMiniGame;

    void Awake()
    {
        LoadMiniGameInfo();
    }

    void OnDestroy()
    {
        SaveMiniGameInfo();
    }

    public bool TryGetMiniGameConfig(MiniGameType type, out MiniGameConfig config)
    {
        return MiniGameConfigsByType.TryGetValue(type, out config);
    }

    void LoadMiniGameInfo()
    {
        MiniGameSaveData miniGameSaveData = Save.GetMiniGameSaveData();
        MiniGameBag = miniGameSaveData.MiniGameBag.Select(mg => (MiniGameType)mg).ToList();
        UnlockedMiniGames = miniGameSaveData.UnlockedMiniGames.Select(mg => (MiniGameType)mg).ToList();
        PracticeMiniGames = miniGameSaveData.PracticeMiniGames.Select(mg => (MiniGameType)mg).ToList();
        LastMiniGame = (MiniGameType)miniGameSaveData.LastMiniGame;
        NextMiniGame = (MiniGameType)miniGameSaveData.NextMiniGame;
    }

    void SaveMiniGameInfo()
    {
        MiniGameSaveData miniGameSaveData = Save.GetMiniGameSaveData();
        miniGameSaveData.MiniGameBag = MiniGameBag.Select(mg => (int)mg).ToArray();
        miniGameSaveData.UnlockedMiniGames =  UnlockedMiniGames.Select(mg => (int)mg).ToArray();
        miniGameSaveData.PracticeMiniGames = PracticeMiniGames.Select(mg => (int)mg).ToArray();
        miniGameSaveData.NextMiniGame = (int)NextMiniGame;
        miniGameSaveData.LastMiniGame = (int)LastMiniGame;
        Save.SaveMiniGameSaveData(miniGameSaveData);
    }

    public void UpdateMiniGames()
    {
        PracticeMiniGames.Clear();
        TryReloadMiniGameBag();
        if (MiniGameBag?.Count > 0)
        {
            NextMiniGame = GetMiniGameFromBag();
            PracticeMiniGames.Add(NextMiniGame);
            for (int i = 1; i < 3; i++)
            {
                TryReloadMiniGameBag();
                PracticeMiniGames.Add(GetMiniGameFromBag());
            }
            LastMiniGame = NextMiniGame;
            PracticeMiniGames.Shuffle();
        }
        SaveMiniGameInfo();
    }

    void TryReloadMiniGameBag()
    {
        if (MiniGameBag == null || MiniGameBag.Count == 0)
        {
            if (UnlockedMiniGames.Count < 3)
            {
                MiniGameBag = new List<MiniGameType>();
                for (int i = 0; i < 3; i++)
                {
                    MiniGameBag.Add(UnlockedMiniGames[UnityEngine.Random.Range(0, UnlockedMiniGames.Count)]);
                }
            }
            else
            {
                MiniGameBag = new List<MiniGameType>(UnlockedMiniGames.Where(mg => mg != LastMiniGame && !PracticeMiniGames.Contains(mg)));
                if (MiniGameBag.Count == 0)
                {
                    MiniGameBag.Add(LastMiniGame);
                }
            }
        }
    }

    MiniGameType GetMiniGameFromBag()
    {
        MiniGameType miniGame = MiniGameBag[UnityEngine.Random.Range(0, MiniGameBag.Count)];
        MiniGameBag.Remove(miniGame);
        return miniGame;
    }
}
