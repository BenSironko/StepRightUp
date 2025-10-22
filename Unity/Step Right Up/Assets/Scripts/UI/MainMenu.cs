using JetBrains.Annotations;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [UsedImplicitly]
    public void GoToPracticeMenu()
    {
        SceneController.LoadScene("PracticeMenu");
    }
}
