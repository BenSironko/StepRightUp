using TMPro;
using UnityEngine;

public class MiniGameCanvas : MonoBehaviour
{
    [SerializeField]
    private StrikeManager m_StrikeManager;
    public StrikeManager StrikeManager => m_StrikeManager;
    
    [SerializeField]
    private TextMeshProUGUI m_TimerText;
    public TextMeshProUGUI TimerText => m_TimerText;
}
