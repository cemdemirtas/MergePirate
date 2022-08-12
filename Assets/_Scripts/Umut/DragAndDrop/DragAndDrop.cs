using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{   
    private GridXZ<GridCell> grid;
    private Transform _pickedUpUnit;
    private GridCell _lastPickedGrid;
    
    

    [SerializeField] private LayerMask _unitLayerMask;
    [SerializeField] private LayerMask _groundLayerMask;
    
    private void Start()
    {
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridBuildingSystem>().grid;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        GridCell gridCell;
        bool unitHit = Physics.Raycast(ray, out RaycastHit hit, 100, _unitLayerMask);
        bool groundHit = Physics.Raycast(ray, out RaycastHit groundHitInfo, 100, _groundLayerMask);

        if (unitHit)
        {
            gridCell = grid.GetGridObject(hit.point);

            if (Input.GetMouseButtonDown(0))
            {
                _lastPickedGrid = gridCell;
                _pickedUpUnit = hit.transform.gameObject.transform;
                gridCell.ClearTransform();
                return;
            }
        }

        if (groundHit)
        {
            gridCell = grid.GetGridObject(groundHitInfo.point);
            if (Input.GetMouseButtonDown(0))
            {
                if (gridCell == null)
                {
                    return;
                }

                _lastPickedGrid = gridCell;
                if (gridCell.isEmpthy())
                {
                    _pickedUpUnit = null;
                }
            }

            if (Input.GetMouseButton(0))
            {
                if (_pickedUpUnit != null)
                {
                    _pickedUpUnit.position = groundHitInfo.point;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (_pickedUpUnit == null)
                {
                    return;
                }
                gridCell = grid.GetGridObject(groundHitInfo.point);

                if (gridCell != null)
                {
                    if (!gridCell.isEmpthy())
                    {
                        _pickedUpUnit.position = grid.GetWorldPositionCenterOfGrid(_lastPickedGrid.x, _lastPickedGrid.z);
                        _lastPickedGrid.SetTransform(_pickedUpUnit);
                    }
                    else
                    {
                        _pickedUpUnit.position =
                            grid.GetWorldPositionCenterOfGrid(gridCell.x, gridCell.z);
                        gridCell.SetTransform(_pickedUpUnit);
                        if (_lastPickedGrid != gridCell)
                        {
                            _lastPickedGrid.ClearTransform();
                        }
                    }
                    
                }
                else
                {
                    _pickedUpUnit.position = grid.GetWorldPositionCenterOfGrid(_lastPickedGrid.x, _lastPickedGrid.z);
                    _lastPickedGrid.SetTransform(_pickedUpUnit);
                }
                _pickedUpUnit = null;
            }
            
            
        }
        
        
    }
}
    