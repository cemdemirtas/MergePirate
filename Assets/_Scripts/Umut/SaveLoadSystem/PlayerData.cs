using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
    public float gold;
    public int currentLevel;
    public GridXZ<GridCell> grid;
    
    
    public PlayerData(GameManager gameManager)
    {
        gold = gameManager.PlayerGold;
        currentLevel = gameManager.CurrentLevel;
        grid = gameManager.Grid;
    }

}
