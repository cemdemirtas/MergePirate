using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportLevelEnemyCount : MonoBehaviour
{
    
    [SerializeField] private int
        goldValue;

    private bool diedOnce = false;
    public bool hpUnder0 = false;
    private bool increasedOnce = false;
    

    private void Update()
    {
        
        if (GameManager.Instance.CurrentGameState == GameState.FightScreen & !increasedOnce)
        {   
            increasedOnce = true;
            Debug.Log(increasedOnce);
            GameManager.Instance.increaseLevelEnemyCount();
        }

        if (hpUnder0 && !diedOnce)
        {
            diedOnce = true;
            GameManager.Instance.decreaseLevelEnemyCount();
            GameManager.Instance.increaseGoldEarnings(goldValue);
            Debug.Log("decreased enemy " + transform.name);

            if (GameManager.Instance.getLevelEnemyCount() <= 0 ||
                GameManager.Instance.getLevelFriendlyUnitCount() <= 0) 
            {
                if (GameManager.Instance.getLevelFriendlyUnitCount() <= 0)
                {   
                    GameManager.Instance.UpdateGameState(GameState.GameOverScreen);
                    this.gameObject.GetComponent<ReportLevelEnemyCount>().enabled = false;
                }

                else if (GameManager.Instance.getLevelEnemyCount() <= 0)
                {
                    GameManager.Instance.UpdateGameState(GameState.GameWonScreen);
                    this.gameObject.GetComponent<ReportLevelEnemyCount>().enabled = false;
                }
            }
        }
    }
}
