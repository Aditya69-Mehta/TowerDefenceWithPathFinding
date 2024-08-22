using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Weapon towerPrefab;

    [SerializeField] bool isPlaceable;
    public bool IsPlaceable {
        get{ // Like A Getter But This Is Called Property. 
            return isPlaceable;
        }
    }

    void OnMouseDown(){
        if(isPlaceable){
            // Debug.Log(name);
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
            isPlaceable = !isPlaced;
        }
    }
}