using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class GameManager : MonoSingleton<GameManager>
{
    public static GameManager instance;
    [SerializeField]
    public bool GameOn;
    public GameState CurrentGameState;
    private int _currentLevel = 1;
    private int levelGoldEarnings = 0;
    private int _boughtMeleeUnitCount = 0;
    private int _boughtRangedUnitCount = 0;
    private float _baseMeleeUnitCost = 100;
    private float _baseRangedUnitCost = 100;
    private int levelEnemyCount = 0;
    private int levelFriendlyUnitCount = 0;
    private float playerGold = 10000;

    [SerializeField] public UnitSO[] _unitSO;
    
    [SerializeField] public SaveGridSO saveGridSO;

    private GridXZ<GridCell> _grid;

    public GridXZ<GridCell> Grid
    {
        get { return _grid; }
        set { _grid = value; }
    }
    public int CurrentLevel
    {
        get { return _currentLevel; }
        set { _currentLevel = value; }
    }

    public float PlayerGold
    {
        get { return playerGold; }
        set { playerGold = value; }
    }

    public int LevelGoldEarnings
    {
        get { return levelGoldEarnings; }
        set { levelGoldEarnings = value; }
    }

    public static event Action<GameState> OnGameStateChanged;



    public void UpdateGameState(GameState newState)
    {
        CurrentGameState = newState;

        switch (newState)
        {
            case GameState.MergeScreen:
                Time.timeScale = 1;
                UIManager.Instance.ShowMergeScreen();
                GameOn = false;
                UIManager.Instance.UpdateGoldIndicator();
                break;
            case GameState.FightScreen:
                Time.timeScale = 1;
                UIManager.Instance.ShowFightScreen();
                UIManager.Instance.UpdateGoldIndicator();

                SetBoolTrue();
                GameOn = true;
                UIManager.Instance.UpdateGoldIndicator();
                break;
            case GameState.GameOverScreen:
                Time.timeScale = 0;
                UIManager.Instance.getGoldEarnings();
                convertGoldEarningsToRealGold();
                resetCountOfUnits();
                UIManager.Instance.ShowDefeatScreen();
                GameOn = false;
                UIManager.Instance.UpdateGoldIndicator();
                break;
            case GameState.GameWonScreen:
                Time.timeScale = 0;
                UIManager.Instance.getGoldEarnings();
                increaseGoldEarnings(levelGoldEarnings); //double profit when won
                convertGoldEarningsToRealGold();
                resetCountOfUnits();
                UIManager.Instance.ShowVictoryScreen();
                GameOn = false;
                UIManager.Instance.UpdateGoldIndicator();
                break;
            case GameState.MainMenuScreen:
                GameOn = false;
                //UIManager.Instance.UpdateGoldIndicator();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }
    
    public void instantateUnit(SaveGridSO saveGridSo, GridXZ<GridCell> _grid)
        {
            foreach (var GridSaveValue in saveGridSo.gridValues)
            {
                for (int i = 0; i < saveGridSo.units.Length; i++)
                {
                    if (GridSaveValue.unitID == saveGridSo.units[i].placedUnit.GetUnitID())
                    {   Debug.Log("unitID: " + GridSaveValue.unitID);
                        PlacedUnit placedUnit = PlacedUnit.Create(
                            _grid.GetWorldPositionCenterOfGrid(GridSaveValue.x,GridSaveValue.z) + new Vector3(0, -0.91f, 0), new Vector2Int(GridSaveValue.x, GridSaveValue.z), saveGridSo.units[i].placedUnit.placedUnitSO);
                        _grid.GetGridObject(GridSaveValue.x,GridSaveValue.z).SetPlacedUnit(placedUnit);
                        GameObject TeamMates = GameObject.Find("TeamMates");
                        if (TeamMates == null)
                            TeamMates = new GameObject("TeamMates");
                        placedUnit.transform.SetParent(TeamMates.transform);
                        TeamMates.tag = "TeamMates";
                    }
                    
                }
                
            }
            saveGridSo.removeAll();
            
        }

    /*public void InstantiatePreviousLevelUnits()
    {
        for (int x = 0;  x < _grid.GetWidth(); x++)
        {
            for (int z = 0; z < _grid.GetHeight(); z++)
            {
                foreach (var unitSO in _unitSO)
                {
                    if (unitSO.unitID == saveGridSO.findUnitIDbyXZ(x,z) )
                    {   PlacedUnit _placedUnit = PlacedUnit.Create(_grid.GetWorldPositionCenterOfGrid(x, z) + new Vector3(0, -0.91f, 0),new Vector2Int(x,z),
                            unitSO);
                        _grid.GetGridObject(x,z).SetPlacedUnit(unitSO.placedUnit);
                    }
                }

                //grid.GetGridObject(x,z).SetPlacedUnit();
            }
                
        }
        GameManager.Instance.saveGridSO.removeAll();
    }*/
    /*public void saveLevelGridCellsandUnits()
    {
        for (int x = 0; x < _grid.GetHeight(); x++)
        {
            for (int z = 0; z < _grid.GetWidth(); z++)
            {
                if (!_grid.GetGridObject(x,z).isEmpthy())
                {
                    saveGridSO.addGridSaveValues(new GridSaveValues(x, z, _grid.GetGridObject(x, z).GetIDPlacedUnit()));
                }
                    
            }
        }
    }*/
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        _currentLevel = data.currentLevel;
        playerGold = data.gold;
        _grid = data.grid;
        SceneManager.LoadScene(_currentLevel);
    }

    public GridXZ<GridCell> getGridObject()
    {
        return _grid;
    }

    public void setGridObject(GridXZ<GridCell> grid)
    {
        _grid = grid;
    }

    public void increaseGoldEarnings(int value)
    {
        levelGoldEarnings += value;
        
    }

    public void resetGoldEarnings()
    {
        levelGoldEarnings = 0;
    }

    public void convertGoldEarningsToRealGold()
    {
        playerGold += levelGoldEarnings;
        resetGoldEarnings();
    }

    public int getLevelEnemyCount()
    {
        return levelEnemyCount;
    }

    public int getLevelFriendlyUnitCount()
    {
        return levelFriendlyUnitCount;
    }

    public void increaseLevelEnemyCount()
    {
        levelEnemyCount += 1;
    }

    public void increaseLevelFriendlyUnitCount()
    {
        levelFriendlyUnitCount += 1;
    }
    public void decreaseLevelEnemyCount()
    {
        levelEnemyCount -= 1;
    }

    public void decreaseLevelFriendlyUnitCount()
    {
        levelFriendlyUnitCount -= 1;
    }

    public void resetCountOfUnits()
    {
        levelEnemyCount = 0;
        levelFriendlyUnitCount = 0;
    }

    public float calculateMeleeUnitCost()
    {
        //Debug.Log("calculateMeleeUnitCost");
        return _baseMeleeUnitCost * Mathf.Pow(1.1f, _boughtMeleeUnitCount + 1);

        //ToDo: re-write cost of melee unit with UI manager -> done
    }

    public float calculateRangedUnitCost()
    {
        return _baseRangedUnitCost * Mathf.Pow(1.1f, _boughtRangedUnitCount + 1);

        //ToDo: re-write cost of ranged unit with UI manager -> done
    }

    public void increaseBoughtMeleeUnitCount()
    {
        _boughtMeleeUnitCount++;
    }

    public void increaseBoughtRangedUnitCount()
    {
        _boughtRangedUnitCount++;
    }

    public void decreasePlayerGold(float amount)
    {
        playerGold -= amount;

        //ToDo: re-write gold value with UI manager
    }

    public void increasePlayerGold(float amount)
    {
        playerGold += amount;

        //ToDo: re-write gold value with UI manager
    }

    public void setPlayerGold(float amount)
    {
        playerGold = amount;
    }

    public void setGrid(GridXZ<GridCell> grid)
    {
        _grid = grid;
    }

    public void clearGrid(GridXZ<GridCell> grid)
    {
        _grid = null;
    }

    public bool checkEnoughGoldForMelee()
    {
        if (calculateMeleeUnitCost() > playerGold)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void NextScene()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        _currentLevel++;
        
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetBoolTrue()
    {
        GameOn = true;
    }

    public void changeCurrentStete(GameState gameState)
    {
        CurrentGameState = gameState;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

}
public enum GameState
{
    MainMenuScreen,
    MergeScreen,
    FightScreen,
    GameOverScreen,
    GameWonScreen
}