using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Save", menuName = "ScriptableObjects/sa", order = 1)]

[System.Serializable]
public class SaveLevelUnitToTransferNextLevelSO : ScriptableObject
{   
    public int[,,] XZandUnitID;
    public UnitSO[] units;
    
}
