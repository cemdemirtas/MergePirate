using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoSingleton<UIManager>
{
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

    [SerializeField] private Button _startFightButton;


    [SerializeField]
    private GameObject _charactersPanel;

    [SerializeField]
    private GameObject _charactersPanelRange;

    [SerializeField]
    private GameObject _charactersPanelMelee;

    [SerializeField]
    private GameObject _settingsPanel;

    [SerializeField] private GameObject _victoryPanel;
    [SerializeField] private GameObject _defeatPanel;

    [SerializeField] private TextMeshProUGUI _victoryEarnedText;
    [SerializeField] private TextMeshProUGUI _defeatEarnedText;
    [SerializeField] private Button _victoryContinueButton;
    [SerializeField] private Button _defeatRestartButton;

    [SerializeField] private ParticleSystem _victoryParticleSystem;
    [SerializeField] private ParticleSystem _defeatParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            UpdateGoldIndicator();
            SetMeleeSoldierCost();
            SetRangedSoldierCost();
        }
    }

    void Update()
    {
        // if (SceneManager.GetActiveScene().buildIndex == 1)
        if (GameManager.Instance.CurrentGameState == GameState.MergeScreen)
        {
            if (_meleeBuyButton.interactable)
            {
                if (GameManager.Instance.PlayerGold - int.Parse(_meleeSoldierCostText.text) < 0)
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
                if (GameManager.Instance.PlayerGold - int.Parse(_rangedSoldierCostText.text) < 0)
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
        GameManager.Instance.PlayerGold =
            GameManager.Instance.PlayerGold - int.Parse(_meleeSoldierCostText.text);
        UpdateGoldIndicator();
        GameManager.Instance.increaseBoughtMeleeUnitCount();
        SetMeleeSoldierCost();
    }

    public void BuyRangeButton()
    {
        GameManager.Instance.PlayerGold =
            GameManager.Instance.PlayerGold - int.Parse(_rangedSoldierCostText.text);
        UpdateGoldIndicator();
        GameManager.Instance.increaseBoughtRangedUnitCount();
        SetRangedSoldierCost();
    }

    public void FightButton()
    {
        /*
        
        START FIGHT HERE
        
        
        */



        //TODO!: disable grid in fight scene
        //disable buy buttons in fight scene
        // _meleeBuyButton.interactable = false;
        // _rangedBuyButton.interactable = false; //! BUG: bu buton paradan bağımsız her türlü açılıyor bu şekilde dikkat et!
    }

    public void ExitFight()
    {
        /*
                
            STOP FIGHT HERE
                
                
        */




        //TODO!: enable grid in after fight scene
        //enable buy buttons in fight scene
        // _meleeBuyButton.interactable = true;
        // _rangedBuyButton.interactable = true; //! BUG: bu buton paradan bağımsız her türlü açılıyor bu şekilde dikkat et!

        //setactive false button in fight scene
        // _meleeBuyButton.gameObject.SetActive(false);
        // _rangedBuyButton.gameObject.SetActive(false);
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

    public void ShowFightScreen()
    {
        if (GameManager.Instance.CurrentGameState == GameState.FightScreen)
        {
            // _meleeBuyButton.gameObject.SetActive(false);
            // _rangedBuyButton.gameObject.SetActive(false);

            _startFightButton.gameObject.SetActive(false);
            _meleeBuyButton.interactable = false;
            _rangedBuyButton.interactable = false;

        }
    }

    public void ShowMergeScreen()
    {
        if (GameManager.Instance.CurrentGameState == GameState.MergeScreen)
        {
            _meleeBuyButton.gameObject.SetActive(true);
            _rangedBuyButton.gameObject.SetActive(true);
        }
    }

    public void ShowVictoryScreen()
    {
        if (GameManager.Instance.CurrentGameState == GameState.GameWonScreen)
        {
            _victoryPanel.SetActive(true);
            //_victoryEarnedText.text = getGoldEarnings().ToString();
            //_victoryContinueButton.gameObject.SetActive(true);
            //disable buy & start fight buttons
            _meleeBuyButton.interactable = false;
            _rangedBuyButton.interactable = false;
            _startFightButton.interactable = false;
            _victoryParticleSystem.Play();
        }
    }

    public void ShowDefeatScreen()
    {

        ;
        if (GameManager.Instance.CurrentGameState == GameState.GameOverScreen)
        {
            _defeatPanel.SetActive(true);
            _defeatEarnedText.text = GameManager.Instance.LevelGoldEarnings.ToString();
            //_defeatRestartButton.gameObject.SetActive(true);
            //disable buy & start fight buttons
            _meleeBuyButton.interactable = false;
            _rangedBuyButton.interactable = false;
            _startFightButton.interactable = false;
            _defeatParticleSystem.Play();

        }
    }

    public void VictoryContinueButton()
    {

        //if scene index is 4, debug log "you won"
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            //load scene 0
            SceneManager.LoadScene(0);
        }
        else
        {
            _victoryPanel.SetActive(false);
            _defeatPanel.SetActive(false);


            _startFightButton.interactable = true;

            _meleeBuyButton.interactable = true;
            _rangedBuyButton.interactable = true;

            GameManager.Instance.UpdateGameState(GameState.MergeScreen);
            GameManager.Instance.NextScene();
        }






    }

    public void DefeatRestartButton()
    {
        _victoryPanel.SetActive(false);
        _defeatPanel.SetActive(false);
        _startFightButton.interactable = true;

        _meleeBuyButton.interactable = true;
        _rangedBuyButton.interactable = true;


    }




    //!!!GOLD STUFF//

    public void UpdateGoldIndicator()
    {
        _goldIndicatorText.text = GameManager.Instance.PlayerGold.ToString();
    }

    public void AddGold(int amount)
    {
        GameManager.Instance.PlayerGold += amount;
        UpdateGoldIndicator();
    }

    public void RemoveGold(int amount) //Gamemanager ile karışmasın diye metot isimleri değiştirildi.
    {
        GameManager.Instance.PlayerGold -= amount;
        UpdateGoldIndicator();
    }

    public int GetGold()
    {
        return (int)(GameManager.Instance.PlayerGold);
    }

    public void SetGold(int amount)
    {
        GameManager.Instance.PlayerGold = amount;
        UpdateGoldIndicator();
    }

    public void getGoldEarnings()
    {
        _victoryEarnedText.text = (GameManager.Instance.LevelGoldEarnings * 2).ToString();
        _defeatEarnedText.text = GameManager.Instance.LevelGoldEarnings.ToString();
    }

    //END GOLD STUFF//

    // COST STUFF //

    public void SetMeleeSoldierCost()
    {
        Debug.Log("SetMeleeSoldierCost");
        _meleeSoldierCostText.text = (
            (int)(GameManager.Instance.calculateMeleeUnitCost())
        ).ToString();
    }

    public void SetRangedSoldierCost()
    {
        Debug.Log("SetRangedSoldierCost");
        //conver to int and then to string
        _rangedSoldierCostText.text = (
            (int)(GameManager.Instance.calculateRangedUnitCost())
        ).ToString();
    }
}
