using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCellHighlight : MonoBehaviour
{   
    [SerializeField] private Material _highlightMaterial;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _borderMaterial;
    private ParticleSystem particleSystem;
    private MeshRenderer _meshRenderer;
    private Collider _collider;
    [SerializeField] private LayerMask _unitLayer;
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        _collider = GetComponent<Collider>();
        _meshRenderer = GetComponent<MeshRenderer>();
        particleSystem = transform.GetComponent<ParticleSystem>();
        
        
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }
    

    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (state == GameState.MergeScreen)
        {
            _meshRenderer.materials[0].color = _defaultMaterial.color;
            _meshRenderer.materials[1].color = _borderMaterial.color;
        }

        if (state == GameState.FightScreen)
        {
            _meshRenderer.materials[0].color = _defaultMaterial.color;
            _meshRenderer.materials[1].color = _defaultMaterial.color;
        }

        else
        {
            _meshRenderer.materials[0].color = _defaultMaterial.color;
            _meshRenderer.materials[1].color = _defaultMaterial.color;
        }
    }

    public void HighlightCell()
    {
        particleSystem.Play();
        _meshRenderer.materials[0].color = _defaultMaterial.color;
        _meshRenderer.materials[1].color = _highlightMaterial.color;
    }
    
    public void UnhighlightCell()
    { 
        particleSystem.Stop();
        
        _meshRenderer.materials[0].color = _defaultMaterial.color;
        _meshRenderer.materials[1].color = _borderMaterial.color;
    }
}
