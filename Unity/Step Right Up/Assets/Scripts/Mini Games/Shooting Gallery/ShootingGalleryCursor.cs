using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShootingGalleryCursor : MonoBehaviour
{
    private ShootingGallery m_ShootingGallery;
    private ShootingGallery ShootingGallery => m_ShootingGallery ? m_ShootingGallery : m_ShootingGallery = GetComponentInParent<ShootingGallery>();
    
    [SerializeField] 
    private float m_MinJitterRadius;
    private float MinJitterRadius => m_MinJitterRadius;
    
    [SerializeField] 
    private float m_MaxJitterRadius;
    private float MaxJitterRadius => m_MaxJitterRadius;
    
    [SerializeField] 
    private float m_MinDeltaMagnitude;
    private float MinDeltaMagnitude => m_MinDeltaMagnitude;
    
    [SerializeField] 
    private float m_MaxDeltaMagnitude;
    private float MaxDeltaMagnitude => m_MaxDeltaMagnitude;
    
    private Camera MainCamera;
    
    void Start()
    {
        MainCamera = Camera.main;
    }
    
    private void Update()
    {
        float jitterAmount = Mathf.InverseLerp(MinDeltaMagnitude * MinDeltaMagnitude, MaxDeltaMagnitude * MaxDeltaMagnitude, Input.mousePositionDelta.sqrMagnitude);
        Vector2 mouseWorldPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 jitterOffset = UnityEngine.Random.insideUnitCircle * Mathf.Lerp(MinJitterRadius, MaxJitterRadius, jitterAmount);
        transform.position = mouseWorldPosition + jitterOffset;
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            CheckCollision();
        }
    }

    void CheckCollision()
    {
        Collider2D[] overlappedColliders = Physics2D.OverlapPointAll(transform.position, LayerMask.GetMask("Shootable"));
        if (overlappedColliders.Length > 0)
        {
            List<ShootingGalleryTarget> targetList = overlappedColliders.Select(oc => oc.GetComponentInParent<ShootingGalleryTarget>()).ToList();
            targetList.RemoveAll(t => t == null);
            targetList = targetList.OrderByDescending(t => t.Layer).ToList();
            ShootingGalleryTarget target = targetList.FirstOrDefault();
            if (target != null)
            {
                target.GotShot();
            }
            else
            {
                MissShot();
            }
        }
        else
        {
            MissShot();
        }
    }

    void MissShot()
    {
        ShootingGallery.GameplayController.Strike();
    }
}
