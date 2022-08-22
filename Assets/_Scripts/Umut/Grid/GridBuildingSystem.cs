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
    [SerializeField] private GameObject gridGroundPrefabEnemyBorderless;

    [SerializeField] private UnitSO[] _unitSO;
    [SerializeField] private SaveGridSO saveGridSO;
    private void Awake()
    {   GameManager.OnGameStateChanged += OnGameStateChanged;
        int gridWidth = 8;
        int gridHeight = 8;
        float cellSizeX = 1.4f;
        float cellSizeZ = 2.2f;
        //float gridCellSize = 10f;
        /*if (GameManager.Instance.getGridObject() == null)
        {
            grid = new GridXZ<GridCell>(gridWidth, gridHeight, cellSizeX,cellSizeZ, originPosition.position, (GridXZ<GridCell> grid, int x, int z) => new GridCell(grid, x, z));
        }
        else
        {
            grid = GameManager.Instance.getGridObject();
        }*/
        
        grid = new GridXZ<GridCell>(gridWidth, gridHeight, cellSizeX,cellSizeZ, originPosition.position, (GridXZ<GridCell> grid, int x, int z) => new GridCell(grid, x, z));

        
        
        
        GameObject gridContainer = GameObject.Find("GridContainer");
        if (gridContainer == null)
            gridContainer = new GameObject("GridContainer");

        for (int i = 0; i < grid.GetWidth(); i++)
        {
            for (int j = 0; j < grid.GetHeight()/2; j++)
            {   GameObject spawnedTile = Instantiate(gridGroundPrefab, grid.GetWorldPositionCenterOfGrid(i, j) + new Vector3(0,-0.91f,0), Quaternion.identity);
                spawnedTile.tag = "GridCellFake";
                spawnedTile.name = $"GridCell [{i}, {j}]";
                spawnedTile.transform.SetParent(gridContainer.transform);
            }

            for (int j = grid.GetHeight()/2; j < gridHeight; j++)
            {
                GameObject spawnedTile = Instantiate(gridGroundPrefabEnemyBorderless, grid.GetWorldPositionCenterOfGrid(i, j) + new Vector3(0,-0.91f,0), Quaternion.identity);
                //spawnedTile.tag = "Grid";
                spawnedTile.name = $"GridCellEnemy [{i}, {j}]";
                spawnedTile.transform.SetParent(gridContainer.transform);
            }
        }
        if (saveGridSO == null)
        {
            saveGridSO = Resources.Load<SaveGridSO>("ScriptableObjects\\GridSaveSO.asset");
            
        }
        if (saveGridSO.getSize()>0)
        {
            GameManager.Instance.instantateUnit(saveGridSO,grid);
        }
    }

    private void OnGameStateChanged(GameState state)
    {
        if (state == GameState.FightScreen)
        {   //GameManager.Instance.saveGridSO.removeAll();
            //GameManager.Instance.setGrid(grid);
            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int z = 0; z < grid.GetHeight()/2; z++)
                {
                    if (!grid.GetGridObject(x, z).isEmpthy())
                    {
                        GridSaveValues gridSaveValues = new GridSaveValues(x,z, grid.GetGridObject(x, z).GetIDPlacedUnit());
                        saveGridSO.addGridSaveValues(gridSaveValues);
                        
                        //GameManager.Instance.saveGridSO.addGridSaveValues(new GridSaveValues(x, z, grid.GetGridObject(x, z).GetIDPlacedUnit()));
                    }
                    
                }
            }
        }

        /*if (state == GameState.MergeScreen)
        {
            for (int x = 0;  x < grid.GetWidth(); x++)
            {
                for (int z = 0; z < grid.GetHeight(); z++)
                {
                    foreach (var unitSO in _unitSO)
                    {
                        if (unitSO.unitID == GameManager.Instance.saveGridSO.findUnitIDbyXZ(x,z) )
                        {
                            grid.GetGridObject(x,z).SetPlacedUnit(unitSO.placedUnit);
                        }
                    }

                    //grid.GetGridObject(x,z).SetPlacedUnit();
                }
                
            }
            GameManager.Instance.saveGridSO.removeAll();
        }*/
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
