using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid{ get{ return grid; } }
    
    void Awake(){
        CreateGrid();
    }

    public Node GetNode(Vector2Int coord){
        if(grid.ContainsKey(coord)){
            return grid[coord];
        }
        return null;
    }

    void CreateGrid()
    {
        for(int x = 0; x < gridSize.x; x++){
            for(int y=0; y < gridSize.y; y++){
                Vector2Int coord = new Vector2Int(x,y);
                grid.Add(coord, new Node(coord, true));
                // Debug.Log(grid[coord].coordinates +" = "+ grid[coord].isWalkable);
            }
        }
    }
}