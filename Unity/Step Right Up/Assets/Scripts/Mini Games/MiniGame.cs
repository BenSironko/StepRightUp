using UnityEngine;

public class MiniGame : MonoBehaviour
{
    [SerializeField]
    private MiniGameType m_Type;
    public MiniGameType Type => m_Type;
}
