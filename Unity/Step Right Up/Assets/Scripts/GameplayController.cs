using System.Collections;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [SerializeField]
    private MiniGameCanvas m_MiniGameCanvas;
    private MiniGameCanvas MiniGameCanvas => m_MiniGameCanvas;

    private MiniGameConfig CurrentConfig;
    
    private int Strikes { get; set; }
    private int MaxStrikes { get; set; }
    private float MaxTime;
    public bool GameRunning { get; set; }
    
    private Coroutine TimerCoroutine;
    
    void Awake()
    {
        if (GameManager.Instance.MiniGameManager.TryGetMiniGameConfig(GameManager.Instance.CurrentMiniGameType, out MiniGameConfig miniGameConfig))
        {
            CurrentConfig = miniGameConfig;
            GameRunning = false;
            if (CurrentConfig.HasStrikes)
            {
                MaxStrikes = CurrentConfig.BaseStrikes;
            }

            if (CurrentConfig.HasTimer)
            {
                MaxTime = CurrentConfig.BaseTimeInSeconds;
                UpdateTimerText(MaxTime);
            }
        }
    }

    void Update()
    {
        if (!GameRunning)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                StartGame();
            }
        }
    }
    
    public void StartGame()
    {
        if (!GameManager.Instance.Practice)
        {
            GameManager.Instance.StartDailyChallenge();
            GameRunning = true;
            if (CurrentConfig.HasTimer)
            {
                StartTimer();
            }
        }
    }

    public void EndGame(bool success)
    {
        if (!GameManager.Instance.Practice)
        {
            GameManager.Instance.EndDailyChallenge(success);
            StopTimer();
            GameRunning = false;
            SceneController.LoadScene("MainMenu");
        }
    }

    IEnumerator Timer()
    {
        float dt = MaxTime;
        while (dt > 0f)
        {
            dt -= Time.deltaTime;
            UpdateTimerText(dt);
            yield return null;
        }
        EndGame(false);
        TimerCoroutine = null;
    }

    void UpdateTimerText(float secondsRemaining)
    {
        int seconds = (int)(secondsRemaining % 60);
        int minutes = (int)secondsRemaining / 60;
        MiniGameCanvas.TimerText.text = minutes.ToString(@"00") + ":" + seconds.ToString(@"00");
    }

    void StartTimer()
    {
        StopTimer();
        TimerCoroutine = StartCoroutine(Timer());
    }

    void StopTimer()
    {
        if (TimerCoroutine != null)
        {
            StopCoroutine(TimerCoroutine);
            TimerCoroutine = null;
        }
    }

    public void Strike()
    {
        if (!GameRunning)
        {
            return;
        }
        Debug.LogError("Miss Shot");
        MiniGameCanvas.StrikeManager.Strike(Strikes);
        Strikes++;
        if (Strikes >= MaxStrikes)
        {
            EndGame(false);
        }
    }
}
