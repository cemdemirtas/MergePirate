using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{   
    private GridXZ<GridBuildingSystem.GridObject> gridObject;
    private Transform _pickedUpUnit;
    private GridBuildingSystem.GridObject _lastPickedGrid;

    [SerializeField] private LayerMask _unitLayerMask;
    [SerializeField] private LayerMask _groundLayerMask;

    private void Start()
    {
        gridObject = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridBuildingSystem>().grid;
    }

    /*private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        GridBuildingSystem.GridObject _gridObject;
        
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _groundLayerMask))
        {
            _gridObject = gridObject.GetGridObject(hit.point);
            if (_gridObject != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _lastPickedGrid = _gridObject;
                    _pickedUpUnit = _gridObject.GetTransform();
                    _pickedUpUnit.GetComponent<Rigidbody>().isKinematic = true;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    _pickedUpUnit.GetComponent<Rigidbody>().isKinematic = false;
                    _pickedUpUnit = null;
                }
                else if (_pickedUpUnit != null)
                {
                    _pickedUpUnit.position = hit.point;
                }
            }
        }
        else if (Physics.Raycast(ray, out hit, Mathf.Infinity, _unitLayerMask))
        {
            _gridObject = gridObject.GetGridObject(hit.point);
            if (_gridObject != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _lastPickedGrid = _gridObject;
                    _pickedUpUnit = _gridObject.GetTransform();
                    _pickedUpUnit.GetComponent<Rigidbody>().isKinematic = true;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    _pickedUpUnit.GetComponent<Rigidbody>().isKinematic = false;
                    _pickedUpUnit = null;
                }
                else if (_pickedUpUnit != null)
                {
                    _pickedUpUnit.position = hit.point;
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                _pickedUpUnit.GetComponent<Rigidbody>().isKinematic = false;
                _pickedUpUnit = null;
            }
        }
    }*/
    
    
}
