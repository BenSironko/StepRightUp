using UnityEngine;

[System.Serializable]
public class MiniGameSaveData
{
    public int NextMiniGame = -1;
    public int LastMiniGame = -1;
    public int[] NextModifiers;
    public int[] PracticeMiniGames;
    public int[] MiniGameBag;
    public int[] UnlockedMiniGames = new int[] { 1, 2, 3 };

    public MiniGameSaveData()
    {
        MiniGameBag = new int[UnlockedMiniGames.Length];
        PracticeMiniGames = new int[UnlockedMiniGames.Length];
        UnlockedMiniGames.CopyTo(MiniGameBag, 0);
        UnlockedMiniGames.CopyTo(PracticeMiniGames, 0);
        PracticeMiniGames.Shuffle();
    }
}
