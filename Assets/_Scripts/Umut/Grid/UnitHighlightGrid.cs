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
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit info, 10, _gridMask))
        {
            _gridCellBelow = info.transform;
            _gridCellBelow.transform.GetComponent<GridCellHighlight>().UnMarkerHighlight();
        }
        
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
        if (_gridCellBelow!=null)
        {
            _gridCellBelow.transform.GetComponent<GridCellHighlight>().UnMarkerHighlight();
        }

        if (_oldGridCellBelow!=null)
        {
            _oldGridCellBelow.transform.GetComponent<GridCellHighlight>().UnMarkerHighlight();
        }
        
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
                    if (info.transform.CompareTag("GridCellFake")) //&&
                        //info.transform.TryGetComponent(out GridCellHighlight gridCellHighlight)) ;//&& info.transform.name.Contains("GridCell"))
                    {   Debug.Log("merge screen on");
                        _gridCellBelow = info.transform;
                        Debug.Log(_gridCellBelow.name);
                
                        _gridCellBelow.transform.GetComponent<GridCellHighlight>().MarkerHighlight();
                    }
                    
                
                }

                if (_gridCellBelow != _tempGridCellBelow)
                {
                    _oldGridCellBelow = _tempGridCellBelow;
                    if (_oldGridCellBelow!=null)
                    {
                        _oldGridCellBelow.transform.GetComponent<GridCellHighlight>().UnMarkerHighlight();
                    }
                
                }

            }
            else
            {
                if (_oldGridCellBelow!= null & _gridCellBelow!=null)
                {
                    _oldGridCellBelow.transform.GetComponent<GridCellHighlight>().UnMarkerHighlight();
                    _gridCellBelow.transform.GetComponent<GridCellHighlight>().UnMarkerHighlight();
                
                }
            
            
            
            }
        }
        
        
        
        

        
        
    }
}