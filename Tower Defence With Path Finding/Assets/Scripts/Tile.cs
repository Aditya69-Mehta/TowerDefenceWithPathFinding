using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Weapon towerPrefab;

    Vector2Int coord = new Vector2Int();

    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get{ return isPlaceable;} }// Like A Getter But This Is Called Property.

    GridManager gridManager;
    PathFinder pathFinder;

    void Awake(){
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void Start(){
        if(gridManager != null){
            coord = gridManager.GetCoordsFromPosition(transform.position);
        }
        if(!isPlaceable){
            gridManager.BlockNode(coord);
        }
    }

    void OnMouseDown(){
        if(gridManager.GetNode(coord).isWalkable && !pathFinder.WillBlockPath(coord)){
            // Debug.Log(name);
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
            if(isPlaced){
                gridManager.BlockNode(coord);
                pathFinder.ChangePath();
            }
        }
    }
}