using JetBrains.Annotations;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [UsedImplicitly]
    public void GoToPracticeMenu()
    {
        GameManager.Instance.SceneController.LoadScene("PracticeMenu");
    }
}
