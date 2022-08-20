using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{   
    [SerializeField] public bool GameOn;
    public GameState CurrentGameState;
    private int _currentLevel;
    private int _boughtMeleeUnitCount = 0;
    private int _boughtRangedUnitCount = 0;
    private float _baseMeleeUnitCost = 100;
    private float _baseRangedUnitCost = 100;
    private float playerGold = 1000;
    public int CurrentLevel
    {
        get { return _currentLevel; }
        set { _currentLevel = value; }
    }
    
    public static event Action<GameState> OnGameStateChanged;
    public enum GameState {MergeScreen, FightScreen, GameOverScreen, GameWonScreen}

    

    public void UpdateGameState(GameState newState)
    {
        CurrentGameState = newState;

        switch (newState)
        {
            case GameState.MergeScreen:
                break;
            case GameState.FightScreen:
                break;
            case GameState.GameOverScreen:
                break;
            case GameState.GameWonScreen:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            
        }

        OnGameStateChanged?.Invoke(newState);

    }

    public float calculateMeleeUnitCost()
    {
        return _baseMeleeUnitCost * Mathf.Pow(1.1f, _boughtMeleeUnitCount +1);
    }

    public float calculateRangedUnitCost()
    {
        return _baseRangedUnitCost * Mathf.Pow(1.1f, _boughtRangedUnitCount +1);
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
    }
    
    public void increasePlayerGold(float amount)
    {
        playerGold += amount;
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
    }
    
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    

    public void SetBoolTrue()
    {
        GameOn = true;
    }

    
}
