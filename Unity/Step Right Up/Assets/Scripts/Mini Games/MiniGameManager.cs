using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField]
    private List<MiniGameConfig> m_MiniGameConfigs;
    private List<MiniGameConfig> MiniGameConfigs => m_MiniGameConfigs;
    
    private List<MiniGameType> MiniGameBag;
    private List<MiniGameType> UnlockedMiniGames;
    private List<MiniGameType> PracticeMiniGames;
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
    }

    public void UpdateMiniGames()
    {
        PracticeMiniGames.Clear();
        TryReloadMiniGameBag();
        if (MiniGameBag?.Count > 0)
        {
            NextMiniGame = GetMiniGameFromBag();
            PracticeMiniGames.Add(NextMiniGame);
            LastMiniGame = NextMiniGame;
            for (int i = 1; i < 3; i++)
            {
                TryReloadMiniGameBag();
                PracticeMiniGames.Add(GetMiniGameFromBag());
            }
            PracticeMiniGames.Shuffle();
        }
        SaveMiniGameInfo();
    }

    void TryReloadMiniGameBag()
    {
        if (MiniGameBag == null || MiniGameBag.Count == 0)
        {
            MiniGameBag = new List<MiniGameType>(UnlockedMiniGames.Where(mg => mg != LastMiniGame));
            if (MiniGameBag.Count == 0 && LastMiniGame != MiniGameType.None)
            {
                MiniGameBag.Add(LastMiniGame);
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
