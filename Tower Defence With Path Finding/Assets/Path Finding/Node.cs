using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // This Is A Pure C# Class Which Is Not Serializable By Default.
public class Node {

    public Vector2Int coordinates;
    public bool isWalkable;
    public bool isExplored;
    public bool isPath;
    public Node connectedTo;

    public Node(Vector2Int coordinates, bool isWalkable){
        this.coordinates = coordinates;
        this.isWalkable = isWalkable;
    }
}
