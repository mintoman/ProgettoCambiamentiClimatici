using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GarbageManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap map;

    [SerializeField]
    private BoardManager boardManager;

    [SerializeField]
    private Garbage garbagePrefab;

    [SerializeField]
    private TileBase brownTile;

    private List<Vector3Int> garbageOnMap = new List<Vector3Int>();

    public float timeBetweenWaves = 1f;
    private float timeToSpawn = 2f;

    public void RemoveFromList(Vector3Int position)
    {
        map.SetTile(position, brownTile);
        garbageOnMap.Remove(position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= timeToSpawn)
        {
            SpawnGarbage();
            timeToSpawn = Time.time + timeBetweenWaves;
        }
    }

    private void SpawnGarbage()
    {
        //print("SpawnGarbage");
        Vector3 pos;
        Vector3Int tilePos;
        TileBase tile;
        string tiletype = "";

        do
        {
            pos = boardManager.RandomPosition();
            tilePos = map.WorldToCell(pos);

            tile = map.GetTile(tilePos);

            if (tile != null)
                tiletype = boardManager.dataFromTiles[tile].type;

        } while (tiletype == "brown" || tiletype == "black" || tile == null);

        CreateGarbage(pos, tilePos);
    }

    void CreateGarbage(Vector3 pos, Vector3Int tilePosition)
    {
        GameObject newGarbage =  ObjectPoolerUpgraded.Instance.SpawnFromPool("Garbage", pos, Quaternion.identity);
        //Garbage newGarbage = Instantiate(garbagePrefab);
        newGarbage.transform.position = map.GetCellCenterWorld(tilePosition);

        IPooledObject pooledObject = newGarbage.GetComponent<IPooledObject>();
        pooledObject.OnObjectSpawn(tilePosition, this);
        //newGarbage.SetOnMap(tilePosition, this);

        garbageOnMap.Add(tilePosition);
    }
}