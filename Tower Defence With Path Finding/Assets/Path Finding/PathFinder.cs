using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoord;
    [SerializeField] Vector2Int endCoord;

    Node startNode;
    Node endNode;
    Node currSearchNode;

    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

    Vector2Int[] searchDirections = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};
    GridManager gridManager;

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    void Awake(){
        gridManager = FindObjectOfType<GridManager>();
        if(gridManager != null){
            grid = gridManager.Grid;
        }
    }

    void Start()
    {
        startNode = gridManager.Grid[startCoord];
        endNode = gridManager.Grid[endCoord];

        BreadthFirstSearch();
        BuildPath();
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
                // Debug.Log("Color" + neighborCoords);
            }
        }

        foreach(Node neighbor in neighbors){
            if(!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable){
                neighbor.connectedTo = currSearchNode;
                frontier.Enqueue(neighbor);
                reached.Add(neighbor.coordinates, neighbor);
            }
        }
    }

    void BreadthFirstSearch(){
        bool isRunning = true;

        frontier.Enqueue(startNode);
        reached.Add(startCoord, startNode);

        while(frontier.Count > 0 && isRunning){
            currSearchNode = frontier.Dequeue();
            currSearchNode.isExplored = true;
            ExploreNeighbors();
            if(currSearchNode.coordinates == endCoord){
                isRunning = false;
            }
        }
    }

    List<Node> BuildPath(){
        List<Node> path = new List<Node>();
        Node currNode = endNode;

        currNode.isPath = true;
        path.Add(currNode);

        while(currNode.connectedTo != null){
            currNode = currNode.connectedTo;
            currNode.isPath = true;
            path.Add(currNode);
        }

        path.Reverse();
        return path;
    }
}
