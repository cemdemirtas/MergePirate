using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateOnGrid : MonoBehaviour
{
    [SerializeField] UnitSO[] units;
    
    [SerializeField] GridXZ<GridBuildingSystem.GridObject> gridObject;

    private void Start()
    {
        gridObject = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridBuildingSystem>().grid;
        
    }

    public void instantiateMelee()
    {   
        if (checkEnoughPlaceToInstantiate(gridObject))
        {   
            selectWhereToInstantiate(units[0].unitPrefab);
        }
        
    }
    public void instantiateRange()
    {   
        if (checkEnoughPlaceToInstantiate(gridObject))
        {   
            selectWhereToInstantiate(units[1].unitPrefab);
        }
        
    }

    private bool checkEnoughPlaceToInstantiate(GridXZ<GridBuildingSystem.GridObject> gridObject)
    {
        bool tempWhileLoop = true;
        bool tempBool = false;
        while (tempWhileLoop)
        {
            for (int x = 0; x < gridObject.GetWidth(); x++)
            {
                for (int z = 0; z < gridObject.GetHeight() - (gridObject.GetHeight()/2); z++)
                {
                    if (gridObject.GetGridObject(x, z).CanBuild() == true)
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
    private void selectWhereToInstantiate(Transform unitPrefab)
    {   bool end = false;
        while (!end)
        {
            int x = UnityEngine.Random.Range(0, gridObject.GetWidth());
            int z = UnityEngine.Random.Range(0, gridObject.GetHeight()/2);
            if (gridObject.GetGridObject(x,z).CanBuild() == true)
            {   
                Transform buildTransform = Instantiate(unitPrefab, gridObject.GetWorldPositionCenterOfGrid(x,z),Quaternion.identity);
                gridObject.GetGridObject(x,z).SetTransform(buildTransform);
                end = true;
            }
            else
            {
                continue;
            }
        }
        
    }
}
