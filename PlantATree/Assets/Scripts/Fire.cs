using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour, IPooledObject
{
    private Vector3Int position;
    private TileData data;
    private FireManager fireManager;

    private float burnTimeCounter, spreadIntervalCounter;

    private void Update()
    {
        burnTimeCounter -= Time.deltaTime;
        if (burnTimeCounter <= 0)
        {
            fireManager.FinishedBurning(position);
            //Destroy(gameObject);//to change to a object pool system
            gameObject.SetActive(false);
        }

        spreadIntervalCounter -= Time.deltaTime;
        if (spreadIntervalCounter <= 0)
        {
            spreadIntervalCounter = data.spreadInterval;
            fireManager.TryToSpread(position, data.spreadChance);
        }
    }

    void IPooledObject.OnObjectSpawnBurn(Vector3Int tilePosition, TileData data, FireManager fm)
    {
        this.position = tilePosition;
        this.data = data;
        fireManager = fm;

        burnTimeCounter = data.burnTime;
        spreadIntervalCounter = data.spreadInterval;
    }

    void IPooledObject.OnObjectSpawn(Vector3Int position, GarbageManager gm)
    {
        throw new System.NotImplementedException();
    }
}
