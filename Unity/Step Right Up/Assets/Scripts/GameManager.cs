using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager m_Instance;

    public static GameManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindFirstObjectByType<GameManager>();
                if (m_Instance == null)
                {
                    m_Instance = new GameObject("GameManager").AddComponent<GameManager>();
                }
            }

            return m_Instance;
        }
        
        private set => m_Instance = value;
    }
    
    //Implicit Properties
    private SceneController m_SceneController;
    public SceneController SceneController => m_SceneController ? m_SceneController : m_SceneController = GetComponent<SceneController>();
    
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneController.LoadScene("MainMenu");
    }

    void OnDestroy()
    {
        Instance = null;
    }
}
