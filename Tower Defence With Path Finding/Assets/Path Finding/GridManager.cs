using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;

    [Tooltip("The Editor Snap Settings")]
    [SerializeField] int editorGridSize = 10;
    public int EditorGridSize { get { return editorGridSize; } }

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

    public void BlockNode(Vector2Int coord){
        if(grid.ContainsKey(coord)){
            grid[coord].isWalkable = false;
        }
    }

    public void ResetNodes(){
        foreach(KeyValuePair<Vector2Int, Node> node in grid){
            node.Value.connectedTo = null;
            node.Value.isExplored = false;
            node.Value.isPath = false;
        }
    }

    public Vector2Int GetCoordsFromPosition(Vector3 position){
        Vector2Int coord = new Vector2Int();
        coord.x = Mathf.RoundToInt(position.x / editorGridSize);
        coord.y = Mathf.RoundToInt(position.z / editorGridSize);

        return coord;
    }

    public Vector3 GetPositionFromCoords(Vector2Int coord){
        Vector3 position = new Vector3Int();
        position.x = coord.x * editorGridSize;
        position.z = coord.y * editorGridSize;
        
        return position;
    }

    void CreateGrid()
    {
        for(int x = 0; x < gridSize.x; x++){
            for(int y = 0; y < gridSize.y; y++){
                Vector2Int coord = new Vector2Int(x,y);
                grid.Add(coord, new Node(coord, true));
                // Debug.Log(grid[coord].coordinates +" = "+ grid[coord].isWalkable);
            }
        }
    }
}