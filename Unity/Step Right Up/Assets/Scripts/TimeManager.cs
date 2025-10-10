using System;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
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

    private TimeSpan WindowStart;
    private TimeSpan WindowEnd;
    public bool InWindow => Save.InWindow;

    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
        WindowStart = new TimeSpan(Config.Data.WindowStartHour, Config.Data.WindowStartMinute, Config.Data.WindowStartSecond);
        WindowEnd = new TimeSpan(Config.Data.WindowEndHour, Config.Data.WindowEndMinute, Config.Data.WindowEndSecond);
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
            if (GameManager.Instance.AttemptedDailyChallenge)
            {
                GameManager.Instance.AttemptedDailyChallenge = false;
            }
            
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
            Save.SaveWindowExit();
            if (OnWindowExited != null)
            {
                OnWindowExited.Invoke();
            }
        }
    }
}