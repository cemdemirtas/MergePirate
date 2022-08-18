using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitScriptableObject", menuName = "ScriptableObjects/UnitScriptableObject", order = 1)]
public class UnitSO : ScriptableObject 
{   
    public string unitName; //Name of the unit
    public string unitType; // Type of the unit
    public int unitID; //ID of the unit
    public int unitHealth;//Health of the unit
    public int unitAttack;//Attack of the unit
    public int unitSpeed;//Speed of the unit
    public int unitRange;//Range of the unit
    public int unitAttackSpeed;
    
    public Transform unitPrefab; //Prefab of the unit
    public PlacedUnit placedUnit; //Placed unit of the unit
    


}
