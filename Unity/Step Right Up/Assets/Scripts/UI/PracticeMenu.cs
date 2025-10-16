using System.Collections.Generic;
using UnityEngine;

public class PracticeMenu : MonoBehaviour
{
    [SerializeField]
    private List<PracticeLevelButton> m_PracticeLevelButtons;
    private List<PracticeLevelButton> PracticeLevelButtons => m_PracticeLevelButtons;

    void Awake()
    {
        for (int i = 0; i < PracticeLevelButtons.Count; i++)
        {
            PracticeLevelButtons[i].SetMiniGame(GameManager.Instance.MiniGameManager.PracticeMiniGames[i]);
        }
    }
}
