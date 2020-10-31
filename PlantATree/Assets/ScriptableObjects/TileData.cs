using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class TileData : ScriptableObject
{
    public TileBase[] tiles;

    public string type = "";

    public bool canBurn;

    public bool canGrow;

    public float growingChance;
    public float erosionChance;

    public float spreadChance, spreadInterval, burnTime;
}
