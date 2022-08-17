using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoSingleton<GameManager>
{

    //scene 0 play exit button
    //scene 1 fight button, 2x buy soldier button + gold indicator(top right)
    //scene2 = scene1 - no fight button, no buy button


    private int gold = 0; //TODO: get gold info from scriptable object

    //TODO!: Ayrı bir scene üzerinde çalışılacak. Umut scene'in den farklı adda scene oluşturulacak.

    [SerializeField] private GameObject _startMenuPanel; //scene 0 (play&exit buttons)
    [SerializeField] private Button _playButton; //scene 0 (play&exit buttons)
    [SerializeField] private Button _exitButton; //scene 0 (play&exit buttons)
    
    [SerializeField] private Gameobject _inGamePanel; //scene 1 (buy panel bottom center) //TODO: panelbakgroun için sprite henüz yok drive'a bakılacak. // olmasada olur saydam düzen denenecek.
    [SerializeField] private Button _buyMeleeButton; //TODO : her iki buton içinde ayrı sprite yapıldı/hazır.
    [SerializeField] private Button _buyRangeButton; //TODO : her iki buton içinde ayrı sprite yapıldı/hazır.
    [SerializeField] private Button _fightButton; //scene 1 (top right button to go scene 1.5)
    [SerializeField] private Button _exitMainMenuButton; //scene 1.5 (top right button to go scene 1 to re-enable buy soldier panel etc.)
    
    [SerializeField] private Gameobject _goldIndicatorPanel; //scene 1 (top right corner gold indicator) //TODO: put gold sprite in this panel
    [SerializeField] private TextMeshProUGUI _goldIndicatorText; //scene 1 (top right corner gold indicator text)

    // Start is called before the first frame update
    void Start()
    {
        _startMenuPanel.SetActive(true);
        _inGamePanel.SetActive(false);
        _goldIndicatorPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowInGamePanel(){
        _inGamePanel.setActive(true);
        _goldIndicatorPanel.SetActive(true);
    }

    public void HideInGamePanel(){
        _inGamePanel.setActive(false);
    }

    public void MainPlayButton(){
        //TODO: load scene 1 and activate _inGamePanel (done)

        //load scene 1
        SceneManager.LoadScene(1);
        //activate _inGamePanel
        ShowInGamePanel();
    }

    public void MainExitButton(){
        //TODO: exit application (done)
        application.Quit();
    }

    public void BuyMeleeButton(){
        //TODO: run instantiateMelee() method from InstantiateOnGrid.cs script
    }

    public void BuyRangeButton(){
        //TODO: run instantiateRange() method from InstantiateOnGrid.cs script
    }

    public void FightButton(){
        HideInGamePanel();
        //TODO!: disable grid in fight scene
    }

    public void ExitMainMenuButton(){
        SceneManager.LoadScene(0);
    }

    //TODO: şimdilik scene1'den scene0'a dönülmüyor. scene1'den scene0'a dönerken soldier pozisyonları ve gold miktarı test edilecek.

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
