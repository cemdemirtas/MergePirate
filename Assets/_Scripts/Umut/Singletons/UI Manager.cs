using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoSingleton<GameManager>
{

    private int gold = 0;



    [SerializeField] private GameObject _startMenuPanel; //scene 0 (play&exit buttons)
    [SerializeField] private Button _playButton; //scene 0 (play&exit buttons)
    [SerializeField] private Button _exitButton; //scene 0 (play&exit buttons)
    
    [SerializeField] private Gameobject _inGamePanel; //scene 1 (buy panel bottom center)
    [SerializeFiled] private Button _buyMeleeButton;
    [SerializeFiled] private Button _buyRangeButton;
    
    [SerializeField] private Gameobject _goldIndicatorPanel; //scene 1 (top right corner gold indicator)
    [SerializeField] private TextMeshProUGUI _goldIndicatorText; //scene 1 (top right corner gold indicator text)

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowInGamePanel(){
        _inGamePanel.setActive(true);
    }

    public void HideInGamePanel(){
        _inGamePanel.setActive(false);
    }

    public void MainPlayButton(){
        //TODO: load scene 1 and activate _inGamePanel
    }

    public void MainExitButton(){
        //TODO: exit application
    }

    public void BuyMeleeButton(){
        //TODO: run instantiateMelee() method from InstantiateOnGrid.cs script
    }

    public void BuyRangeButton(){
        //TODO: run instantiateRange() method from InstantiateOnGrid.cs script
    }

    public void ShowGoldIndicator(){
        _goldIndicatorPanel.setActive(true);
    }

    private void UpdateGoldIndicator(){
        _goldIndicatorText.text = gold.ToString();
    }

    public void AddGold(int amount){
        gold += amount;
        UpdateGoldIndicator();
    }

    public void RemoveGold(int amount){
        gold -= amount;
        UpdateGoldIndicator();
    }

    public int GetGold(){
        return gold;
    }

    public void SetGold(int amount){
        gold = amount;
        UpdateGoldIndicator();
    }
}
