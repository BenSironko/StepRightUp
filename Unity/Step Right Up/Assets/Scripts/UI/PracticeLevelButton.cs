using JetBrains.Annotations;
using UnityEngine;
using TMPro;

public class PracticeLevelButton : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI m_TitleText;
    private TextMeshProUGUI TitleText => m_TitleText;
    
    private MiniGameType MiniGameType;

    public void SetMiniGame(MiniGameType miniGameType)
    {
        MiniGameType = miniGameType;
        if (GameManager.Instance.MiniGameManager.TryGetMiniGameConfig(MiniGameType, out MiniGameConfig config))
        {
            TitleText.text = config.MiniGameName;
        }
    }

    [UsedImplicitly]
    public void OnClicked()
    {
        if (MiniGameType != MiniGameType.None)
        {
            GameManager.Instance.Practice = true;
            GameManager.Instance.SceneController.LoadMiniGame(MiniGameType);
        }
    }
}
