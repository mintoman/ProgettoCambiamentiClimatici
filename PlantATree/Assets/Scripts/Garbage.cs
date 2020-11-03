using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour, IPooledObject
{
    private GarbageManager garbageManager;

    private float lifeTimeCounter;

    private Vector3Int position;

    public void OnObjectSpawn(Vector3Int position, GarbageManager gm)
    {
        this.position = position;
        garbageManager = gm;
        lifeTimeCounter = 5f;
    }

    public void SetOnMap(Vector3Int position, GarbageManager gm)
    {
        /*this.position = position;
        garbageManager = gm;
        lifeTimeCounter = 2f;*/
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimeCounter -= Time.deltaTime;
        if (lifeTimeCounter <= 0)
        {
            gameObject.SetActive(false);
            garbageManager.RemoveFromList(position);
            //Destroy(gameObject);//to change to a object pool system
        }
    }

    void IPooledObject.OnObjectSpawnBurn(Vector3Int tilePosition, TileData data, FireManager fm)
    {
        throw new System.NotImplementedException();
    }
}
