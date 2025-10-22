using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShootingGallery : MiniGame
{
    [SerializeField]
    private List<ShootingGalleryTarget> m_Targets;
    private List<ShootingGalleryTarget> Targets => m_Targets;

    void Start()
    {
        foreach (ShootingGalleryTarget target in Targets)
        {
            target.OnShot.AddListener(OnShot);
        }
    }

    void OnShot()
    {
        if (!GameplayController.GameRunning)
        {
            return;
        }
        if (Targets.All(t => t.Shot))
        {
            FinishMiniGame();
        }
    }
}