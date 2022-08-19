using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] public bool GameOn;
    public enum gameState {mergeScreen, fight}

    public void SetBoolTrue()
    {
        GameOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
