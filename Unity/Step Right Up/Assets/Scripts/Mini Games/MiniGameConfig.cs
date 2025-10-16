using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "New Mini Game", menuName = "Mini Games/Mini Game")]
[System.Serializable]
public class MiniGameConfig : ScriptableObject
{
    [SerializeField]
    private MiniGameType m_Type;
    public MiniGameType Type => m_Type;
    
    [SerializeField]
    private List<ModifierType> m_PossibleModifiers;
    public List<ModifierType> PossibleModifiers => m_PossibleModifiers;
}
