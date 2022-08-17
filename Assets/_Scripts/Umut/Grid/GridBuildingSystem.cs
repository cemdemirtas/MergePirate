using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using CodeMonkey.Utils;
public class GridBuildingSystem  : MonoSingleton<GridBuildingSystem>
{
    public GridXZ<GridCell> grid;
    [SerializeField] private Transform originPosition;
    [SerializeField] private GameObject gridGroundPrefab;
    private void Awake()
    {
        int gridWidth = 8;
        int gridHeight = 8;
        float cellSizeX = 1.4f;
        float cellSizeZ = 2.2f;
        //float gridCellSize = 10f;
        grid = new GridXZ<GridCell>(gridWidth, gridHeight, cellSizeX,cellSizeZ, originPosition.position, (GridXZ<GridCell> grid, int x, int z) => new GridCell(grid, x, z));
        
        GameObject gridContainer = GameObject.Find("GridContainer");
        if (gridContainer == null)
            gridContainer = new GameObject("GridContainer");

        for (int i = 0; i < grid.GetWidth(); i++)
        {
            for (int j = 0; j < grid.GetHeight(); j++)
            {   GameObject spawnedTile = Instantiate(gridGroundPrefab, grid.GetWorldPositionCenterOfGrid(i, j), Quaternion.identity);
                spawnedTile.tag = "Grid";
                spawnedTile.name = $"GridCell [{i}, {j}]";
                spawnedTile.transform.SetParent(gridContainer.transform);
            }
        }
    }

    
    
    /*public void InstantiateGridObjectRandomly(Transform gridObjectPrefab)
    {   bool end = false;
        while (!end)
        {
            for (int x = 0; x < grid.GetHeight() - (grid.GetHeight()/2) ; x++)
            {
                for (int z = 0; z < grid.GetHeight() - (grid.GetHeight()/2); z++)
                {
                    if (grid.GetGridObject(x,z).CanBuild())
                    {
                        Transform buildTransform = Instantiate(gridObjectPrefab, grid.GetWorldPositionCenterOfGrid(x,z),Quaternion.identity);
                        grid.GetGridObject(x,z).SetTransform(buildTransform);
                        end = true;
                    }
                    
                }
            }
        }
        
    }*/
    
    
    /*private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {   
            grid.GetXZ(Mouse3D.GetMouseWorldPosition(),out int x, out int z);
            GridObject gridObject = grid.GetGridObject(x, z);
            if (gridObject.CanBuild())
            {   Transform buildTransform = Instantiate(gridObjectPrefab, grid.GetWorldPositionCenterOfGrid(x,z),Quaternion.identity);
                gridObject.SetTransform(buildTransform);
            }
            else
            {
                UtilsClass.CreateWorldTextPopup("Cannot merge!", Mouse3D.GetMouseWorldPosition());
            }
            
        }
    }*/

    
    
    
}
