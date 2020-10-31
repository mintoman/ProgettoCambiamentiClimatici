using UnityEngine;
using System.Collections;

public interface IPooledObject
{
    void OnObjectSpawn(Vector3Int position, GarbageManager gm);
}
