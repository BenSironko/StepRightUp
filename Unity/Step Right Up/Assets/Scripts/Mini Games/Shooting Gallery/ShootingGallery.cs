using UnityEngine;

public class ShootingGallery : MonoBehaviour, IMiniGame
{
    [SerializeField]
    private MiniGameType m_Type;
    public MiniGameType Type => m_Type;
}