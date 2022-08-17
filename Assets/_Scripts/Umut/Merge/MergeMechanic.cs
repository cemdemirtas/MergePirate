using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeMechanic : MonoBehaviour
{
    private GridXZ<GridCell> gridObject;
    [SerializeField] UnitSO[] units;
    
    private void Start()
    {
        gridObject = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridBuildingSystem>().grid;
        
    }
    
    
    
    
}
