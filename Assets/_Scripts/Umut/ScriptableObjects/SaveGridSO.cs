using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitScriptableObject", menuName = "ScriptableObjects/GridSaveObject", order = 1)]

[System.Serializable]
public class SaveGridSO: ScriptableObject
{
    public List<GridSaveValues> gridValues;
    public UnitSO[] units;


    public void removeAll()
    {
        gridValues.Clear();
    }
    public int getSize()
    {
        return gridValues.Count;
    }
    
    public void addGridSaveValues(GridSaveValues gsv)
    {   
        gridValues.Add(gsv);
    }

    public int findUnitIDbyXZ(int x, int z)
    {
        int tempUnitId = new int();
        for (int i = 0; i < gridValues.Count ; i++)
        {
            if (gridValues[i].x == x & gridValues[i].z == z)
            {
                
                tempUnitId = gridValues[i].unitID;
                
            }
        }

        return tempUnitId;
    }
    
    
}
[System.Serializable]
public class GridSaveValues
{
    public int x;
    public int z;
    public int unitID;
    
    public GridSaveValues(int x, int z, int unitID)
    {
        this.x = x;
        this.z = z;
        this.unitID = unitID;
    }
}
