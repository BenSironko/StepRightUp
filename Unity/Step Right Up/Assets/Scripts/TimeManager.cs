using System;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    private const float GameSecondsPerRealSeconds = 60f*60f;
    
    private static TimeManager m_Instance;
    public static TimeManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindFirstObjectByType<TimeManager>();
            }
            return m_Instance;
        }
        
        private set => m_Instance = value;
    }
    
    [SerializeField]
    private UnityEvent m_OnWindowEntered;
    public UnityEvent OnWindowEntered
    {
        get
        {
            if (m_OnWindowEntered == null)
            {
                m_OnWindowEntered = new UnityEvent();
            }
            return m_OnWindowEntered;
        }
    }
    
    [SerializeField]
    private UnityEvent m_OnWindowExited;
    public UnityEvent OnWindowExited
    {
        get
        {
            if (m_OnWindowExited == null)
            {
                m_OnWindowExited = new UnityEvent();
            }
            return m_OnWindowExited;
        }
    }
    
    public bool Paused {get; set;}

    //public Day CurrentDay { get; private set; } = Day.Monday;
    private TimeSpan CurrentTime;
    public float CurrentSeconds => CurrentTime.Seconds;
    public float CurrentMinutes => CurrentTime.Minutes;
    public float CurrentHour => CurrentTime.Hours;
    public string TimeString => DateTime.Today.Add(CurrentTime).ToString(@"h\:mm tt");

    private TimeSpan WindowStart;
    private TimeSpan WindowEnd;
    private bool InWindow;

    private void OnEnable()
    {
        Instance = this;
        CurrentTime = new TimeSpan(0, 0, 0);
        DontDestroyOnLoad(gameObject);
        InWindow = Save.InWindow;
        WindowStart = new TimeSpan(Config.Data.WindowStartHour, Config.Data.WindowStartMinute, Config.Data.WindowStartSecond);
        WindowEnd = new TimeSpan(Config.Data.WindowEndHour, Config.Data.WindowEndMinute, Config.Data.WindowEndSecond);
    }

    void OnDisable()
    {
        Instance = null;
    }

    void Update()
    {
        TimeSpan now = DateTime.Now.TimeOfDay;
        // string currentUtcTime = DateTime.UtcNow.ToString();
        // DateTime currentConvertedTime = new DateTime(DateTime.Parse(currentUtcTime).Ticks, DateTimeKind.Utc);
        // Debug.LogError(DateTime.Now.ToString() + " " + DateTime.UtcNow.ToString() + " " + currentConvertedTime.ToLocalTime().Hour.ToString());
        if (WindowStart <= WindowEnd)
        {
            if (now >= WindowStart && now <= WindowEnd)
            {
                TryEnterWindow();
            }
            else
            {
                TryExitWindow();
            }
        }
        else
        {
            if (now >= WindowStart || now <= WindowEnd)
            {
                TryEnterWindow();
            }
            else
            {
                TryExitWindow();
            }
        }
    }

    void TryEnterWindow()
    {
        if (!InWindow)
        {
            InWindow = true;
            Save.SaveWindowEntry();
            if (OnWindowEntered != null)
            {
                OnWindowEntered.Invoke();
            }
        }
    }

    void TryExitWindow()
    {
        if (InWindow)
        {
            InWindow = false;
            Save.SaveWindowExit();
            if (OnWindowExited != null)
            {
                OnWindowExited.Invoke();
            }
        }
    }
}