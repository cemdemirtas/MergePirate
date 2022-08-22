using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportLevelFriendCount : MonoBehaviour
{
    
    private bool diedOnce = false;
    public bool hpUnder0 = false;
    private bool increasedOnce = false;
    

    
    private void FixedUpdate()
    {
        if (GameManager.Instance.CurrentGameState == GameState.FightScreen & !increasedOnce)
        {
            increasedOnce = true;
            GameManager.Instance.increaseLevelFriendlyUnitCount();
        }

        if (hpUnder0 && !diedOnce)
        {
            diedOnce = true;
            GameManager.Instance.decreaseLevelFriendlyUnitCount();
            Debug.Log("decreased friendly " + transform.name);
            if (GameManager.Instance.getLevelEnemyCount() <= 0 ||
                GameManager.Instance.getLevelFriendlyUnitCount() <= 0) 
            {   
                if (GameManager.Instance.getLevelFriendlyUnitCount() <= 0)
                {
                    GameManager.Instance.UpdateGameState(GameState.GameOverScreen);
                    this.gameObject.GetComponent<ReportLevelFriendCount>().enabled = false;
                }

                if (GameManager.Instance.getLevelEnemyCount() <= 0)
                {
                    GameManager.Instance.UpdateGameState(GameState.GameWonScreen);
                    this.gameObject.GetComponent<ReportLevelFriendCount>().enabled = false;
                }
            }
        }
    }
}
