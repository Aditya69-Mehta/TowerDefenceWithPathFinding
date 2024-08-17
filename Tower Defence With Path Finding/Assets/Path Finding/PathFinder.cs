using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Node currSearchNode;
    Vector2Int[] searchDirections = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};
    GridManager gridManager;

    Dictionary<Vector2Int, Node> grid;

    void Awake(){
        gridManager = FindObjectOfType<GridManager>();
        if(gridManager != null){
            grid = gridManager.Grid;
        }
    }

    void Start()
    {
        ExploreNeighbors();
    }

    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();
        
        foreach(Vector2Int direction in searchDirections){
            Vector2Int neighborCoords = currSearchNode.coordinates + direction;

            if(grid.ContainsKey(neighborCoords)){
                neighbors.Add(grid[neighborCoords]);

                //TODO: Remove after testing.
                // Tester(neighborCoords);
                grid[neighborCoords].isExplored = true;
                grid[currSearchNode.coordinates].isPath = true;
                Debug.Log("Color");
            }else{
                Debug.Log("No");
            }
        }
    }

    void Tester(Vector2Int neighborCoords){
        grid[neighborCoords].isExplored = true;
        grid[currSearchNode.coordinates].isPath = true;
    }
}
