using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoSingleton<GameManager>
{
    //scene 0 play exit button
    //scene 1 fight button, 2x buy soldier button + gold indicator(top right)
    //scene2 = scene1 - no fight button, no buy button



    //! gold eksiye düşmesin
    //! gold yetersiz olduğunda initprefab eklemesin


    private int gold = 0; //TODO: get gold info from somewhere else

    //TODO!: Ayrı bir scene üzerinde çalışılacak. Umut scene'in den farklı adda scene oluşturulacak. (kaan adında oluşturulacak)





    [SerializeField]
    private GameObject _goldIndicatorPanel; //scene 1 (top right corner gold indicator) //TODO: put gold sprite in this panel

    [SerializeField]
    private TextMeshProUGUI _goldIndicatorText; //scene 1 (top right corner gold indicator text)

    [SerializeField]
    private TextMeshProUGUI _meleeSoldierCostText;

    [SerializeField]
    private TextMeshProUGUI _rangedSoldierCostText;

    [SerializeField]
    private Button _meleeBuyButton;

    [SerializeField]
    private Button _rangedBuyButton;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            SetGold(500);
        }
    }

    void Update()
    {
        //chech if _meleeSoldierCostText.text is active
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (_meleeBuyButton.interactable)
            {
                if (gold - int.Parse(_meleeSoldierCostText.text) < 0)
                {
                    //Disable buy button
                    _meleeBuyButton.interactable = false;
                }
                else
                {
                    _meleeBuyButton.interactable = true;
                }
            }

            if (_rangedBuyButton.interactable)
            {
                if (gold - int.Parse(_rangedSoldierCostText.text) < 0)
                {
                    //Disable buy button
                    _rangedBuyButton.interactable = false;
                }
                else
                {
                    _rangedBuyButton.interactable = true;
                }
            }
        }
    }

    public void MainPlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void MainExitButton()
    {
        //TODO: exit application (done)
        Application.Quit();
    }

    public void BuyMeleeButton()
    {
        gold = gold - int.Parse(_meleeSoldierCostText.text);
        UpdateGoldIndicator();
    }

    public void BuyRangeButton()
    {
        gold = gold - int.Parse(_rangedSoldierCostText.text);
        UpdateGoldIndicator();
    }

    public void FightButton()
    {
        /*
        
        START NAVMESH FIGHT HERE
        
        
        */
        //TODO!: disable grid in fight scene
        //disable buy buttons in fight scene
        _meleeBuyButton.interactable = false;
        _rangedBuyButton.interactable = false;
    }

    public void ExitFight()
    {
        /*
                
            STOP NAVMESH FIGHT HERE
                
                
        */


        //TODO!: enable grid in after fight scene
        //enable buy buttons in fight scene
        _meleeBuyButton.interactable = true;
        _rangedBuyButton.interactable = true;
    }

    //GOLD STUFF//

    private void UpdateGoldIndicator()
    {
        _goldIndicatorText.text = gold.ToString();
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateGoldIndicator();
    }

    public void RemoveGold(int amount)
    {
        gold -= amount;
        UpdateGoldIndicator();
    }

    public int GetGold()
    {
        return gold;
    }

    public void SetGold(int amount)
    {
        gold = amount;
        UpdateGoldIndicator();
    }
}
