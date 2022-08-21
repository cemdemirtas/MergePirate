using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{   
    private GridXZ<GridCell> grid;
    private PlacedUnit _pickedUpUnit;
    private GridCell _lastPickedGrid;
    [SerializeField] private UnitSO[] _units;
    [SerializeField] private float yAdjustment;
    public List<int> unitIdIndex = new List<int>();
    private GameObject teammates;


    [SerializeField] private LayerMask _unitLayerMask;
    [SerializeField] private LayerMask _groundLayerMask;

    private void Awake()
    {
        for (int i = 0; i < _units.Length; i++)
        {
            unitIdIndex.Add(_units[i].unitID);
        }
        
        
    }

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
                _pickedUpUnit = hit.transform.gameObject.GetComponent<PlacedUnit>();
                gridCell.ClearPlacedUnit();
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
                    _pickedUpUnit.transform.position = groundHitInfo.point + Vector3.up * 2f;
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
                    if (gridCell.x < grid.GetWidth() && gridCell.x >= 0 && gridCell.z < grid.GetHeight() / 2 &&
                        gridCell.z >= 0)
                    {
                        if (!gridCell.isEmpthy())
                        {
                            if (gridCell.GetIDPlacedUnit() != _pickedUpUnit.GetUnitID())
                            {
                                _pickedUpUnit.transform.position =
                                    grid.GetWorldPositionCenterOfGrid(_lastPickedGrid.x, _lastPickedGrid.z) +
                                    new Vector3(0, yAdjustment, 0);
                                _lastPickedGrid.SetPlacedUnit(_pickedUpUnit);
                            }
                            else if (_pickedUpUnit.GetUnitID() == 31 || _pickedUpUnit.GetUnitID() == 32)
                            {
                                _pickedUpUnit.transform.position =
                                    grid.GetWorldPositionCenterOfGrid(_lastPickedGrid.x, _lastPickedGrid.z) +
                                    new Vector3(0, yAdjustment, 0);
                                _lastPickedGrid.SetPlacedUnit(_pickedUpUnit);
                            }
                            else if (_pickedUpUnit.GetUnitID() == gridCell.GetIDPlacedUnit())
                            {   
                                int temp = _pickedUpUnit.GetUnitID();
                                int index = System.Array.IndexOf(unitIdIndex.ToArray(), temp + 10);
                                //System.Array.FindLastIndex()
                                Destroy(gridCell.GetPlacedUnit().transform.gameObject);
                                gridCell.ClearPlacedUnit();
                                Transform parent = _pickedUpUnit.gameObject.transform.root;
                                Destroy(_pickedUpUnit.gameObject);
                                _pickedUpUnit = _units[index].placedUnit;
                                PlacedUnit placedUnit = PlacedUnit.Create(
                                    grid.GetWorldPositionCenterOfGrid(gridCell.x, gridCell.z) + new Vector3(0,yAdjustment,0),
                                    new Vector2Int(gridCell.x, gridCell.z), _pickedUpUnit.placedUnitSO);
                                gridCell.SetPlacedUnit(placedUnit);
                                placedUnit.transform.SetParent(parent);
                                _pickedUpUnit = null;
                            }
                        }
                        else
                        {
                            _pickedUpUnit.transform.position =
                                grid.GetWorldPositionCenterOfGrid(gridCell.x, gridCell.z) + new Vector3(0, yAdjustment, 0);
                            gridCell.SetPlacedUnit(_pickedUpUnit);
                            lookForTeamMates();
                            _pickedUpUnit.transform.SetParent(teammates.transform);
                            if (_lastPickedGrid != gridCell)
                            {
                                _lastPickedGrid.ClearPlacedUnit();
                            }
                        }

                    }
                    else
                    {
                        _pickedUpUnit.transform.position =
                            grid.GetWorldPositionCenterOfGrid(_lastPickedGrid.x, _lastPickedGrid.z) + new Vector3(0,yAdjustment,0);
                        _lastPickedGrid.SetPlacedUnit(_pickedUpUnit);
                    }
                }
                else
                {   
                    _pickedUpUnit.transform.position = grid.GetWorldPositionCenterOfGrid(_lastPickedGrid.x, _lastPickedGrid.z) + new Vector3(0,yAdjustment,0);
                    _lastPickedGrid.SetPlacedUnit(_pickedUpUnit);
                }
                _pickedUpUnit = null; 
            }
        }
        
    }

    private void lookForTeamMates()
    {
        GameObject teammates = GameObject.Find("TeamMates");
        if (teammates == null)
            teammates = new GameObject("TeamMates");
    }
        
        
    }

    