using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void LoadMiniGame(MiniGameType miniGameType)
    {
        GameManager.Instance.CurrentMiniGameType = miniGameType;
        LoadScene("Gameplay");
    }
}
