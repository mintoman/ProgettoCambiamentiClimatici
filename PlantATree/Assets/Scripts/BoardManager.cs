﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Tilemaps;

public class BoardManager : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int minimum;             //Minimum value for our Count class.
        public int maximum;             //Maximum value for our Count class.


        //Assignment constructor.
        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public int columns = 12;                                         //Number of columns in our game board.
    public int rows = 10;                                            //Number of rows in our game board.

    public Count flowerCount = new Count(5, 10);                        //Lower and upper limit for our random number of seeds per level.
    public Count brownCount = new Count(1, 3);                        //Lower and upper limit for our random number of garbage items per level.

    private Transform boardHolder;                                    //A variable to store a reference to the transform of our Board object.
    private List<Vector3> gridPositions = new List<Vector3>();    //A list of possible locations to place tiles.

    public Tilemap tilemap;
    public GameObject seed;
    public GameObject garbage;

    public TileBase flowerTile;
    public TileBase greenTile;
    public TileBase brownTile;
    public TileBase blackTile;

    //private Dictionary<Vector3Int, float> erodedTiles = new Dictionary<Vector3Int, float>();
    private Dictionary<Vector3Int, float> flowerTiles = new Dictionary<Vector3Int, float>();
    private Dictionary<Vector3Int, float> brownTiles = new Dictionary<Vector3Int, float>();

    [SerializeField]
    private List<TileData> tileDatas;

    public Dictionary<TileBase, TileData> dataFromTiles;

    // Start is called before the first frame update
    void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();
        foreach (var tileData in tileDatas)
        {
            foreach (var tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);//the tile will be the key the tileData the value
            }
        }
    }

    //RandomPosition returns a random position from our list gridPositions.
    public Vector3 RandomPosition()
    {
        //Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
        int randomIndex = Random.Range(0, gridPositions.Count);

        //Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
        Vector3 randomPosition = gridPositions[randomIndex];
        
        //Remove the entry at randomIndex from the list so that it can't be re-used.
        //gridPositions.RemoveAt(randomIndex);
        
        //Return the randomly selected Vector3 position.
        return randomPosition;
    }

    //Clears our list gridPositions and prepares it to generate a new board.
    void InitialiseList()
    {
        //Clear our list gridPositions.
        gridPositions.Clear();

        Debug.Log("row: "+(-9 + tilemap.size.x - 1));
        Debug.Log("column: " + (-5 + tilemap.size.y - 4));
        //Loop through x axis (columns).
        for (int x = -9; x <= -9 + tilemap.size.x - 1; x++)
        {
            //Within each column, loop through y axis (rows).
            for (int y = -5; y <= -5 + tilemap.size.y - 4; y++)
            {
                //At each index add a new Vector3 to our list with the x and y coordinates of that position.
                gridPositions.Add(new Vector3(x, y, 0f));

                Vector3Int tilePos = tilemap.WorldToCell(new Vector3(x, y, 0f));
                tilemap.SetTile(tilePos, greenTile);
                //Debug.Log(new Vector3(x, y, 0f));
            }
        }  
    }

    void LayoutTilesAtRandom(Dictionary<Vector3Int, float> tiles, TileBase newtile, int minimum, int maximum)
    {
        //Choose a random number of objects to instantiate within the minimum and maximum limits
        int objectCount = Random.Range(minimum, maximum + 1);
        Debug.Log("Tiles tot: "+ objectCount);
        //Instantiate objects until the randomly chosen limit objectCount is reached
        for (int i = 0; i < objectCount; i++)
        {
            //Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
            Vector3 randomPosition = RandomPosition();
            Vector3Int tilePos = tilemap.WorldToCell(randomPosition);

            while (tiles.ContainsKey(tilePos))
            {
               randomPosition = RandomPosition();
               tilePos = tilemap.WorldToCell(randomPosition);
               //tilemap.WorldToCell(tilemap.LocalToWorld(randomPosition));        
            }

            tiles.Add(tilePos, 1f);
            tilemap.SetTile(tilePos, newtile);

            //Choose a random tile from tileArray and assign it to tileChoice
            //GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];

            //Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
            //Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }

    //SetupScene initializes our level and calls the previous functions to lay out the game board
    public void SetupScene(int level)
    {
        //Creates the outer walls and floor.
        //BoardSetup();

        //Reset our list of gridpositions.
        InitialiseList();

        //Instantiate a random number of wall tiles based on minimum and maximum, at randomized positions.
        LayoutTilesAtRandom(flowerTiles, flowerTile, flowerCount.minimum, flowerCount.maximum);

        LayoutTilesAtRandom(brownTiles, brownTile, brownCount.minimum, brownCount.maximum);

        //Instantiate a random number of food tiles based on minimum and maximum, at randomized positions.
        //LayoutObjectAtRandom(foodTiles, garbageCount.minimum, garbageCount.maximum);

        //Determine number of enemies based on current level number, based on a logarithmic progression
        //int enemyCount = (int)Mathf.Log(level, 2f);

        //Instantiate a random number of enemies based on minimum and maximum, at randomized positions.
        //LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);

        //Instantiate the exit tile in the upper right hand corner of our game board
        //Instantiate(exit, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);
    }

    private void Start()
    {
        
        SetupScene(0);
        //print("tilemap bounds: " + tilemap.size);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public TileData GetTileData(Vector3Int tilePosition)
    {
        TileBase tile = tilemap.GetTile(tilePosition);

        if (tile == null)
            return null;
        else
            return dataFromTiles[tile];
    }
}