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
                UIManager.Instance.ShowMergeScreen();
                GameOn = false;
                UIManager.Instance.UpdateGoldIndicator();
                break;
            case GameState.FightScreen:
                UIManager.Instance.ShowFightScreen();
                UIManager.Instance.UpdateGoldIndicator();
                SetBoolTrue();
                GameOn = true;
                UIManager.Instance.UpdateGoldIndicator();
                break;
            case GameState.GameOverScreen:
                UIManager.Instance.getGoldEarnings();
                convertGoldEarningsToRealGold();
                resetCountOfUnits();
                UIManager.Instance.ShowDefeatScreen();
                GameOn = false;
                UIManager.Instance.UpdateGoldIndicator();
                break;
            case GameState.GameWonScreen:
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
        Debug.Log("GoldEarnings: " + levelGoldEarnings);
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
        SavePlayer();
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