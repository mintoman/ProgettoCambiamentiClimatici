using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FireManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap map;

    [SerializeField]
    private TileBase blackTile;
    [SerializeField]
    private TileBase brownTile;

    [SerializeField]
    private BoardManager boardManager;

    [SerializeField]
    private Fire firePrefab;

    private List<Vector3Int> activeFires = new List<Vector3Int>();

    public void FinishedBurning(Vector3Int position)
    {
        map.SetTile(position, blackTile);
        activeFires.Remove(position);
    }

    public void TryToSpread(Vector3Int position, float spreadChance)
    {
        for (int x = position.x -1; x < position.x + 2; x++) 
        {
            for (int y = position.y - 1; y < position.y + 2; y++)
            {
                TryToBurnTile(new Vector3Int(x, y, 0));
            }
        }

        //c# local function a function within a function
        void TryToBurnTile(Vector3Int tilePosition)
        {
            
            if (activeFires.Contains(tilePosition)) return;
            
            TileData data = boardManager.GetTileData(tilePosition);

            if(data != null && data.canBurn)
            {
                if (UnityEngine.Random.Range(0f, 100f) <= data.spreadChance)
                {

                    SetTileOnFire(tilePosition, data);
                    //map.SetTile(tilePosition, brownTile);
                }
                    
            }
        }
    }

    public void SetTileOnFire(Vector3Int tilePosition, TileData data)
    {
        //Fire newFire = Instantiate(firePrefab);
        GameObject newFire = ObjectPoolerUpgraded.Instance.SpawnFromPool("Fire", new Vector3(0f,0f,0f), Quaternion.identity);
        
        //newFire.StartBurning(tilePosition, data, this);
        IPooledObject pooledObject = newFire.GetComponent<IPooledObject>();
        pooledObject.OnObjectSpawnBurn(tilePosition, data, this);

        newFire.transform.position = map.GetCellCenterWorld(tilePosition);
        map.SetTile(tilePosition, blackTile);

        activeFires.Add(tilePosition);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = map.WorldToCell(mousePosition);

            TileData data = boardManager.GetTileData(gridPosition);
            if (data != null && data.canBurn)
            {
                SetTileOnFire(gridPosition, data);
            }
        }*/
    }
}