using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleEffect : MonoBehaviour
{
    public Color red;
    public Color green;
    public Color white;

    [SerializeField] private MeshRenderer _meshRenderer;
    
    private void Awake()
    {
        HitColor();
    }


    public void ResetColor()
    {
        _meshRenderer.material.color = white;
    }
    
    public void HitColor()
    {
        StartCoroutine(HandleHitColor());
    }


    public void MissColor()
    {
        StartCoroutine(HandleMissColor());
    }
    
    
    IEnumerator HandleMissColor()
    {
        _meshRenderer.material.color = red;
        yield return new WaitForSeconds(0.2f);
        _meshRenderer.material.color = white;
        
    }
    
    IEnumerator HandleHitColor()
    {
        _meshRenderer.material.color = green;
        yield return new WaitForSeconds(0.2f);
        _meshRenderer.material.color = white;
        
    }
    
    
}
