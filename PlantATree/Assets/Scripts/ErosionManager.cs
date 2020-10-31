using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ErosionManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap map;

    private Dictionary<Vector3Int, float> erodedTiles = new Dictionary<Vector3Int, float>();

    // Start is called before the first frame update
    void ErodeTile(Vector3Int gridPosition)
    {
        if(!erodedTiles.ContainsKey(gridPosition))//if not eroded
        {
            erodedTiles.Add(gridPosition, 1f);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
