using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateOnGrid : MonoBehaviour
{
    [SerializeField] UnitSO[] units;
    
    private GridXZ<GridCell> gridObject;

    private void Start()
    {
        gridObject = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridBuildingSystem>().grid;
        
    }

    public void instantiateMelee()
    {   
        if (checkEnoughPlaceToInstantiate(gridObject))
        {   
            selectWhereToInstantiate(units[0].placedUnit);
        }
        
    }
    public void instantiateRange()
    {   
        if (checkEnoughPlaceToInstantiate(gridObject))
        {   
            selectWhereToInstantiate(units[1].placedUnit);
        }
        
    }

    private bool checkEnoughPlaceToInstantiate(GridXZ<GridCell> gridObject)
    {
        bool tempWhileLoop = true;
        bool tempBool = false;
        while (tempWhileLoop)
        {
            for (int x = 0; x < gridObject.GetWidth(); x++)
            {
                for (int z = 0; z < gridObject.GetHeight() - (gridObject.GetHeight()/2); z++)
                {
                    if (gridObject.GetGridObject(x, z).isEmpthy() == true)
                    {   
                        tempBool= true;
                        tempWhileLoop = false;
                    }
                }
            }
            break;
        }

        return tempBool;

    }
    private void selectWhereToInstantiate(PlacedUnit placedUnit)
    {   bool end = false;
        while (!end)
        {
            int x = UnityEngine.Random.Range(0, gridObject.GetWidth());
            int z = UnityEngine.Random.Range(0, gridObject.GetHeight()/2);
            if (gridObject.GetGridObject(x,z).isEmpthy() == true)
            {
                PlacedUnit _placedUnit = PlacedUnit.Create(gridObject.GetWorldPositionCenterOfGrid(x, z) + new Vector3(0, 1, 0),new Vector2Int(x,z),
                    placedUnit.placedUnitSO);
                //Transform buildTransform = Instantiate(unitPrefab, gridObject.GetWorldPositionCenterOfGrid(x,z)  + new Vector3(0,1,0),Quaternion.identity);
                gridObject.GetGridObject(x,z).SetPlacedUnit(_placedUnit);
                end = true;
            }
            else
            {
                continue;
            }
        }
        
    }
}
