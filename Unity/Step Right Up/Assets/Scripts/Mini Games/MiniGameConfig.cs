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
    private string m_MiniGameName;
    public string MiniGameName => m_MiniGameName;
    
    [SerializeField]
    private MiniGame m_Prefab;
    public MiniGame Prefab => m_Prefab;
    
    [SerializeField]
    private bool m_HasStrikes;
    public bool HasStrikes => m_HasStrikes;
    
    [SerializeField]
    private int m_BaseStrikes = 3;
    public int  BaseStrikes => m_BaseStrikes;
    
    [SerializeField]
    private bool m_HasTimer;
    public bool HasTimer => m_HasTimer;
    
    [SerializeField]
    private float m_BaseTimeInSeconds;
    public float BaseTimeInSeconds => m_BaseTimeInSeconds;
    
    [SerializeField]
    private List<ModifierType> m_PossibleModifiers;
    public List<ModifierType> PossibleModifiers => m_PossibleModifiers;
}
