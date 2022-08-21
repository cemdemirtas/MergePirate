using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHighlightGrid : MonoBehaviour
{
    [SerializeField] private LayerMask _gridMask;
    private Transform _gridCellBelow;
    private Transform _oldGridCellBelow;
    private Transform _tempGridCellBelow;
    private int _yValueWhenPlacedGrid;
    private bool mergeScreenOn;

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        GameManagerOnGameStateChanged(GameManager.Instance.CurrentGameState);
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (state == GameState.MergeScreen)
        {
            mergeScreenOn = true;
        }
        else
        {
            mergeScreenOn = false;
        }
    }

    private void Start()
    {
        _yValueWhenPlacedGrid = (int) transform.position.y;
        
    }

    private void FixedUpdate()
    {
        if (mergeScreenOn)
        {
            if (_yValueWhenPlacedGrid+ 0.4f < transform.position.y)
            {
                if (_gridCellBelow != null)
                {
                    _tempGridCellBelow = _gridCellBelow;
                }
                if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit info ,10, _gridMask))
                {
                    if (info.transform.CompareTag("Grid") && info.transform.name.Contains("GridCell"))
                    {
                        _gridCellBelow = info.transform;
                
                        _gridCellBelow.transform.GetComponent<GridCellHighlight>().HighlightCell();
                    }
                
                }

                if (_gridCellBelow != _tempGridCellBelow)
                {
                    _oldGridCellBelow = _tempGridCellBelow;
                    if (_oldGridCellBelow!=null)
                    {
                        _oldGridCellBelow.transform.GetComponent<GridCellHighlight>().UnhighlightCell();
                    }
                
                }

            }
            else
            {
                if (_oldGridCellBelow!= null & _gridCellBelow!=null)
                {
                    _oldGridCellBelow.transform.GetComponent<GridCellHighlight>().UnhighlightCell();
                    _gridCellBelow.transform.GetComponent<GridCellHighlight>().UnhighlightCell();
                
                }
            
            
            
            }
        }
        
        
        
        

        
        
    }
}