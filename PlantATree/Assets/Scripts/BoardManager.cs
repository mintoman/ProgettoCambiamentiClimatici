using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class BoardManager : MonoBehaviour
{
    public int columns = 12;                                         //Number of columns in our game board.
    public int rows = 10;                                            //Number of rows in our game board.

    private List<Vector3> gridPositions = new List<Vector3>();    //A list of possible locations to place tiles.

    public Tilemap tilemap;

    public TileBase flowerTile;
    public TileBase greenTile;
    public TileBase brownTile;
    public TileBase blackTile;

    [SerializeField]
    private List<TileData> tileDatas;

    public Dictionary<TileBase, TileData> dataFromTiles;

    public float timeBetweenWaves = 1f;
    private float timeToSpawn = 3f;

    [SerializeField]
    private FireManager fireManager;

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= timeToSpawn)
        {
            CheckBoard();
            timeToSpawn = Time.time + timeBetweenWaves;
        }
    }

    private void CheckBoard()
    {
        int countGoodTerrainTile = 0;
        for (int index = 0; index < gridPositions.Count; index++)
        {
            Vector3 pos = gridPositions[index];
            Vector3Int gridPosition = tilemap.WorldToCell(pos);
            //print("index: "+index+" tile grid pos: "+ gridPosition);
            TileData data = GetTileData(gridPosition);
            if (data != null && data.canBurn)
            {
                //print(data);

                switch (data.type)
                {
                    case "green":
                        countGoodTerrainTile++;
                        if (Random.Range(0f, 100f) <= data.growingChance)
                        {
                            tilemap.SetTile(gridPosition, flowerTile);
                        }
                        else if (Random.Range(0f, 100f) <= data.erosionChance)
                        {
                            tilemap.SetTile(gridPosition, brownTile);
                        }
                        break;
                    case "flower":
                        countGoodTerrainTile++;
                        if (Random.Range(0f, 100f) <= data.erosionChance)
                        {
                            tilemap.SetTile(gridPosition, greenTile);
                        }
                        break;
                    case "brown":
                        countGoodTerrainTile++;
                        if (Random.Range(0f, 100f) <= data.erosionChance)
                        {
                            //start a fire
                            fireManager.SetTileOnFire(gridPosition, data);
                        }
                        break;
                    case "black":
                        break;
                }    
            }
        }

        if (countGoodTerrainTile <= 45)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();
        foreach (var tileData in tileDatas)
        {
            foreach (var tile in tileData.tiles)
            {
                print("adding to dictionary: "+ tileData.type);
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

        //Loop through x axis (columns).
        for (int x = -9; x < -9 + tilemap.size.x - 1; x++)
        //for (int x = -9; x < -9*2; x++)
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
        Debug.Log("tilemap.size.x " + tilemap.size.x+ " tilemap.size.y " + tilemap.size.y);
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
        }
    }

    //SetupScene initializes our level and calls the previous functions to lay out the game board
    public void SetupScene(int level)
    {
        //Creates the outer walls and floor.
        //BoardSetup();

        //Reset our list of gridpositions.
        InitialiseList();
    }

    private void Start()
    {
        
        SetupScene(0);
        //print("tilemap bounds: " + tilemap.size);
    }

    public TileBase GetTile(Vector2 worldPosition)
    {
        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);

        TileBase tile = tilemap.GetTile(gridPosition);

        if (tile == null)
            return null;
        else
            return tile;
    }

    public TileData GetTileData(Vector3Int tilePosition)
    {
        TileBase tile = tilemap.GetTile(tilePosition);

        if (tile == null)
            return null;
        else
            return dataFromTiles[tile];
    }

    public TileData GetTileData(Vector3 worldPosition)
    {
        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);

        TileBase tile = tilemap.GetTile(gridPosition);

        if (tile == null)
            return null;
        else
            return dataFromTiles[tile];
    }

    public void SetGreenTileData(Vector3 position)
    {
        Vector3Int tilePos = tilemap.WorldToCell(position);
        tilemap.SetTile(tilePos, greenTile);
    }
}