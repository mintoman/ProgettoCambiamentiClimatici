﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private Vector3Int position;
    private TileData data;
    private FireManager fireManager;

    private float burnTimeCounter, spreadIntervalCounter;

    public void StartBurning(Vector3Int position, TileData data, FireManager fm)
    {
        this.position = position;
        this.data = data;
        fireManager = fm;

        burnTimeCounter = data.burnTime;
        spreadIntervalCounter = data.spreadInterval;
    }

    private void Update()
    {
        burnTimeCounter -= Time.deltaTime;
        if (burnTimeCounter <= 0)
        {
            fireManager.FinishedBurning(position);
            Destroy(gameObject);//to change to a object pool system
        }

        spreadIntervalCounter -= Time.deltaTime;
        if (spreadIntervalCounter <= 0)
        {
            spreadIntervalCounter = data.spreadInterval;
            fireManager.TryToSpread(position, data.spreadChance);
        }
    }
}
