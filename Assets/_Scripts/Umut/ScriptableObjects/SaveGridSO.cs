using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitScriptableObject", menuName = "ScriptableObjects/GridSaveObject", order = 1)]
public class SaveGridSO: ScriptableObject
{
    public List<GridSaveValues> gridValues;
    
    
    public void removeAll()
    {
        gridValues.Clear();
    }
    
    public void addGridSaveValues(GridSaveValues gsv)
    {
        gridValues.Add(gsv);
    }
    
}
public class GridSaveValues
{
    public int x;
    public int y;
    public GridCell Unit;
    
    public GridSaveValues(int x, int y, GridCell Unit)
    {
        this.x = x;
        this.y = y;
        this.Unit = Unit;
    }
}
