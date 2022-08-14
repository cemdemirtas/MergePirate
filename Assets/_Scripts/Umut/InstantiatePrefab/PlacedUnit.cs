using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedUnit : MonoBehaviour
{   
    public static PlacedUnit Create(Vector3 worldPosition, Vector2Int origin, UnitSO placedUnitSo)
    {
        Transform placedUnitTransform = Instantiate(placedUnitSo.unitPrefab, worldPosition, Quaternion.identity);
        PlacedUnit placedUnit = placedUnitTransform.GetComponent<PlacedUnit>();
        placedUnit.origin = origin;
        placedUnit.placedUnitSO = placedUnitSo;
        
        return placedUnit;
    }
    public UnitSO placedUnitSO;
    private Vector2Int origin;
    
    public int GetUnitID()
    {
        return placedUnitSO.unitID;
    }
}
