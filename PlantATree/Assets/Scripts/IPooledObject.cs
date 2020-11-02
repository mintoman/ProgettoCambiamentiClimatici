using UnityEngine;
using System.Collections;

public interface IPooledObject
{
    void OnObjectSpawn(Vector3Int position, GarbageManager gm);
    void OnObjectSpawnBurn(Vector3Int tilePosition, TileData data, FireManager fm);
}
