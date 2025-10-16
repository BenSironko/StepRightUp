using UnityEngine;

public class GameplayController : MonoBehaviour
{
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
