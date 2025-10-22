using UnityEngine;

public class MiniGame : MonoBehaviour
{
    private GameplayController m_GameplayController;

    public GameplayController GameplayController
    {
        get
        {
            if (m_GameplayController == null)
            {
                m_GameplayController = GetComponentInParent<GameplayController>();
            }
            return m_GameplayController;
        }
    }
    
    [SerializeField]
    private MiniGameType m_Type;
    public MiniGameType Type => m_Type;

    public void FinishMiniGame()
    {
        GameplayController.EndGame(true);
    }
}
