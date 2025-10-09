using System.Collections.Generic;
using UnityEngine;

public class PracticeMenu : MonoBehaviour
{
    [SerializeField]
    private List<PracticeLevelButton> m_PracticeLevelButtons;
    private List<PracticeLevelButton> PracticeLevelButtons => m_PracticeLevelButtons;
}
