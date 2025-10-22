using System.Collections.Generic;
using UnityEngine;

public class StrikeManager : MonoBehaviour
{
    [SerializeField]
    private List<Strike> m_Strikes;
    private List<Strike> Strikes => m_Strikes;

    public void Strike(int index)
    {
        if (Strikes.Count > index)
        {
            Strikes[index].StrikeOut();
        }
    }
}
