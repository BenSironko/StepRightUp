using UnityEngine;

public class GameplayController : MonoBehaviour
{
    void Awake()
    {
        if (GameManager.Instance.MiniGameManager.TryGetMiniGameConfig(GameManager.Instance.CurrentMiniGameType, out MiniGameConfig miniGameConfig))
        {
        }
    }
    
    public void StartGame()
    {
        if (!GameManager.Instance.Practice)
        {
            GameManager.Instance.StartDailyChallenge();
        }
    }

    public void EndGame(bool success)
    {
        if (!GameManager.Instance.Practice)
        {
            GameManager.Instance.EndDailyChallenge(success);
        }
    }
}
