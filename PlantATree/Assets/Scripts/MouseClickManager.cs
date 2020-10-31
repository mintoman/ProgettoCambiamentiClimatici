﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseClickManager : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase blackTile;

    [SerializeField]
    private List<TileData> tileDatas;

    private Dictionary<TileBase, TileData> dataFromTiles;

    // Start is called before the first frame update
    void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();

        foreach(var tileData in tileDatas)
        {
            foreach (var tile in tileData.tiles)
            {
                dataFromTiles.Add(tile,tileData);//the tile will be the key the tileData the value
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = tilemap.WorldToCell(mousePosition);

            TileBase clickedTile = tilemap.GetTile(gridPosition);

            if (clickedTile != null)
            {
                string tiletype = dataFromTiles[clickedTile].type;
                print("At position: " + gridPosition + " clickedTile " + clickedTile);
                print("tile type: " + tiletype + " on clickedTile " + clickedTile);
            }

            tilemap.SetTile(gridPosition, blackTile);
        }
    }

    public string GetTileType(Vector2 worldPosition)
    {
        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);

        TileBase tile = tilemap.GetTile(gridPosition);

        if (tile == null)
            return "null";

        string tiletype = dataFromTiles[tile].type;

        return tiletype;
    }

}