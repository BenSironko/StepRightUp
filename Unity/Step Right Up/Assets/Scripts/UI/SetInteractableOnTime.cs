using System;
using UnityEngine;
using UnityEngine.UI;

public class SetInteractableOnTime : MonoBehaviour
{
    private Selectable m_Selectable;
    private Selectable Selectable => m_Selectable ? m_Selectable : m_Selectable = GetComponent<Selectable>();
    
    enum Time
    {
        InWindow,
        OutsideWindow
    }
    
    [SerializeField]
    private Time m_ActiveTime = Time.OutsideWindow;
    private Time ActiveTime => m_ActiveTime;

    private void Update()
    {
        TryUpdateInteractable();
    }

    void TryUpdateInteractable()
    {
        switch (ActiveTime)
        {
            case Time.InWindow:
                Selectable.interactable = GameManager.Instance.InWindow;
                break;
            
            case Time.OutsideWindow:
                Selectable.interactable = !GameManager.Instance.InWindow;
                break;
        }
    }
}
