using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CordDisp : MonoBehaviour
{
    Color defaultColor = Color.white;
    Color blockedColor = Color.gray;
    Color exploredColor = Color.yellow;
    Color pathColor = Color.magenta; // Orange new Color(1f, .5f, 0f)

    Vector2Int coord = new Vector2Int();
    TextMeshPro coordText;


    GridManager gridManager;


    void Awake(){
        coordText = GetComponent<TextMeshPro>();
        coordText.enabled = false;
        
        gridManager = FindObjectOfType<GridManager>();
        DispCoord();
    }

    void Update()
    {
        if(!Application.isPlaying){
            DispCoord();
            coordText.enabled =true;
        }

        // if(Application.isPlaying) coordText.enabled = false;
        ToggleCoord();
        ChangeCoordColor();
    }

    void ToggleCoord()
    {
        if(Input.GetKeyDown(KeyCode.C)){
            coordText.enabled = !coordText.IsActive();
        }
    }

    void ChangeCoordColor()
    {
        if(gridManager == null) return;

        Node node = gridManager.GetNode(coord);
        if(node == null) return;

        if(!node.isWalkable){
            coordText.color = blockedColor;
        }else if(node.isPath){
            coordText.color = pathColor;
        }else if(node.isExplored){
            coordText.color = exploredColor;
        }else{
            coordText.color = defaultColor;
        }
    }

    void DispCoord()
    {
        coord.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coord.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
        coordText.text = coord.x + ", " + coord.y;// $"{coord.x}, {coord.y}";
        transform.parent.name = coord.ToString();// "("+coordText.text+")";
    }
}
