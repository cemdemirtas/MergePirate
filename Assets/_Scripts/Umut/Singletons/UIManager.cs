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
    private GameObject _startMenuPanel; //scene 0 (play&exit buttons)

    [SerializeField]
    private GameObject _inGamePanel; //scene 1 (buy panel bottom center) //TODO: panelbakgroun için sprite henüz yok drive'a bakılacak. // olmasada olur saydam düzen denenecek.

    [SerializeField]
    private GameObject _goldIndicatorPanel; //scene 1 (top right corner gold indicator) //TODO: put gold sprite in this panel

    [SerializeField]
    private TextMeshProUGUI _goldIndicatorText; //scene 1 (top right corner gold indicator text)

    [SerializeField]
    private TextMeshProUGUI _meleeSoldierCostText;

    [SerializeField]
    private TextMeshProUGUI _rangedSoldierCostText;

    // Start is called before the first frame update
    void Start()
    {
        // _startMenuPanel.SetActive(true);
        // _inGamePanel.SetActive(false);
        // _goldIndicatorPanel.SetActive(false);

        //scenemanager

        SetGold(5000);
    }

    // Update is called once per frame
    void Update() { }

    public void ShowInGamePanel()
    {
        _inGamePanel.SetActive(true);
        _goldIndicatorPanel.SetActive(true);
    }

    public void HideInGamePanel()
    {
        _inGamePanel.SetActive(false);
    }

    public void MainPlayButton()
    {
        //TODO: load scene 1 and activate _inGamePanel (done)

        //load scene 1
        //SceneManager.LoadScene(1);
        SceneManager.LoadScene(1);

        //activate _inGamePanel
        ShowInGamePanel();
    }

    public void MainExitButton()
    {
        //TODO: exit application (done)
        Application.Quit();
    }

    public void BuyMeleeButton()
    {
        //TODO: run instantiateMelee() method from InstantiateOnGrid.cs script
        gold = gold - int.Parse(_meleeSoldierCostText.text);
        UpdateGoldIndicator();
    }

    public void BuyRangeButton()
    {
        //TODO: run instantiateRange() method from InstantiateOnGrid.cs script
        gold = gold - int.Parse(_rangedSoldierCostText.text);
        UpdateGoldIndicator();
    }

    public void FightButton()
    {
        HideInGamePanel();
        //TODO!: disable grid in fight scene
    }

    public void ExitMainMenuButton()
    {
        //SceneManager.LoadScene(0);
    }

    public void ShowGoldIndicator()
    {
        _goldIndicatorPanel.SetActive(true);
    }

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
