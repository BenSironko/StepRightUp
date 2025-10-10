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
            }

            return m_Instance;
        }
        
        private set => m_Instance = value;
    }
    
    //Implicit Properties
    private SceneController m_SceneController;
    public SceneController SceneController => m_SceneController ? m_SceneController : m_SceneController = GetComponent<SceneController>();
    
    private TimeManager m_TimeManager;
    private TimeManager TimeManager => m_TimeManager ? m_TimeManager : m_TimeManager = GetComponent<TimeManager>();
    
    //State
    public bool InWindow => TimeManager.InWindow;
    public bool InGame { get; set; }
    public bool AttemptedDailyChallenge { get => Save.AttemptedDailyChallenge; set => Save.AttemptedDailyChallenge = value; }
    
    public bool TimeForRealAttempt => TimeManager.InWindow && !AttemptedDailyChallenge;
    
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneController.LoadScene("MainMenu");
        TimeManager.OnWindowEntered.AddListener(OnWindowEntered);
        TimeManager.OnWindowExited.AddListener(OnWindowExited);
    }

    void OnDestroy()
    {
        Instance = null;
        TimeManager.OnWindowEntered.RemoveListener(OnWindowEntered);
        TimeManager.OnWindowExited.RemoveListener(OnWindowExited);
    }
    
    public void StartDailyChallenge()
    {
        InGame = true;
    }

    public void EndDailyChallenge(bool success)
    {
        InGame = false;
        AttemptedDailyChallenge = false;

        if (success)
        {
            
        }
        else
        {
            
        }
    }

    void OnWindowEntered()
    {
        
    }

    void OnWindowExited()
    {
        
    }
}
