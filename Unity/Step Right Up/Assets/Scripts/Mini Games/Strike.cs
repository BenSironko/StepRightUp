using UnityEngine;
using UnityEngine.UI;

public class Strike : MonoBehaviour
{
    private Image m_Image;
    private Image Image => m_Image ? m_Image : GetComponent<Image>();
    
    public void Awake()
    {
        Image.color = Color.green;
    }

    public void StrikeOut()
    {
        Image.color = Color.red;
    }
}
