using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoSingleton<GameManager>
{
    private int gold = 0; //TODO: get gold info from somewhere else

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

    [SerializeField]
    private GameObject _charactersPanel;

    [SerializeField]
    private GameObject _charactersPanelRange;

    [SerializeField]
    private GameObject _charactersPanelMelee;

    [SerializeField]
    private GameObject _settingsPanel;

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
        
        START FIGHT HERE
        
        
        */
        //TODO!: disable grid in fight scene
        //disable buy buttons in fight scene
        _meleeBuyButton.interactable = false;
        _rangedBuyButton.interactable = false;
    }

    public void ExitFight()
    {
        /*
                
            STOP FIGHT HERE
                
                
        */


        //TODO!: enable grid in after fight scene
        //enable buy buttons in fight scene
        _meleeBuyButton.interactable = true;
        _rangedBuyButton.interactable = true;
    }

    public void CharacterPanel()
    {
        if (!_settingsPanel.activeSelf)
        {
            _charactersPanel.SetActive(!_charactersPanel.activeSelf);
        }
    }

    public void CharacterPanelRange()
    {
        _charactersPanelRange.SetActive(true);

        if (_charactersPanelRange.activeSelf)
        {
            _charactersPanelMelee.SetActive(false);
        }
    }

    public void CharacterPanelMelee()
    {
        _charactersPanelMelee.SetActive(true);

        if (_charactersPanelMelee.activeSelf)
        {
            _charactersPanelRange.SetActive(false);
        }
    }

    public void SettingsPanel()
    {
        if (!_charactersPanel.activeSelf)
        {
            _settingsPanel.SetActive(!_settingsPanel.activeSelf);
        }
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
