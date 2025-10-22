using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class ShootingGalleryTarget : MonoBehaviour
{
    [SerializeField]
    private int m_Layer;
    public int Layer
    {
        get => m_Layer;
        private set => m_Layer = value;
    }
    
    [SerializeField]
    private SortingGroup m_SortingGroup;
    private SortingGroup SortingGroup => m_SortingGroup;
    
    public bool Shot { get; private set; }

    private UnityEvent m_OnShot;
    public UnityEvent OnShot
    {
        get
        {
            if (m_OnShot == null)
            {
                m_OnShot = new UnityEvent();
            }
            return m_OnShot;
        }
        
        private set => m_OnShot = value;
    }

    void Start()
    {
        Shot = false;
        SortingGroup.sortingOrder = Layer;
    }
    
#if UNITY_EDITOR
    void Update()
    {
        SortingGroup.sortingOrder = Layer;
    }
#endif

    void OnDestroy()
    {
        Shot = false;
        OnShot.RemoveAllListeners();
    }
    
    public void GotShot()
    {
        SortingGroup.gameObject.SetActive(false);
        Shot = true;
        OnShot.Invoke();
    }
}
