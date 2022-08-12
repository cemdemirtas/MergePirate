using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell
{   
    private GridXZ<GridCell> grid;
    public int x;
    public int z;
    private Transform transform;
    private UnitSO unit;
    


    public GridCell(GridXZ<GridCell> grid, int x, int z)
    {   
            
        this.grid = grid;
        this.x = x; 
        this.z = z;
    }
    public void SetTransform(Transform transform)
    {
        this.transform = transform;
        
        grid.TriggerGridObjectChanged(x,z);
    }
        
    public void ClearTransform()
    {
        this.transform = null;
        
        grid.TriggerGridObjectChanged(x,z);
    }

    public bool isEmpthy()
    {
        return transform == null;
    }
        
    public Transform GetTransform()
    {
        return transform;
    }
        
    public void MoveTransform(Vector3 position)
    {
        transform.position = position;
        grid.TriggerGridObjectChanged(x,z);
    }
    
    public void ChangeTransform(GridXZ<GridCell> grid,  int oldX, int oldZ, int newX, int newZ, Transform objectTransform)
    {
        GridCell oldCell = grid.GetGridObject(oldX, oldZ);
        oldCell.ClearTransform();
        GridCell newCell = grid.GetGridObject(newX, newZ);
        newCell.SetTransform(objectTransform);
        grid.TriggerGridObjectChanged(oldX, oldZ);
        grid.TriggerGridObjectChanged(newX, newZ);
    }
    
    public void ChangeTransform(GridXZ<GridCell> grid,  GridCell oldGridCell, GridCell newGridCell, Transform objectTransform)
    {
        oldGridCell.ClearTransform();
        newGridCell.SetTransform(objectTransform);
        grid.TriggerGridObjectChanged(oldGridCell.x, oldGridCell.z);
        grid.TriggerGridObjectChanged(newGridCell.x, newGridCell.z);
        
    }

    public bool canMerge(GridCell oldGridCell, GridCell newGridCell)
    {
        return oldGridCell.GetTransform() == newGridCell.GetTransform();
    }
    
        
    public override string ToString()
    {
        return x + "," + z + "\n"+ transform;
    }
    
    
    
        
}
