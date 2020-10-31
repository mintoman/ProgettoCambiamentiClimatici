using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridMovement : MonoBehaviour
{
    public Tilemap tilemap;

    public BoardManager boardManager;

    private bool isMoving;
    private Vector3 originPos;
    private Vector3 targetPos;
    public float timeToMove = 0.25f;

    [SerializeField]
    private float refireRate = 2f;
    private float fireTimer = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) && !isMoving)
            StartCoroutine(MovePlayer(Vector3.up));

        else if (Input.GetKey(KeyCode.A) && !isMoving)
            StartCoroutine(MovePlayer(Vector3.left));

        else if (Input.GetKey(KeyCode.S) && !isMoving)
            StartCoroutine(MovePlayer(Vector3.down));

        else if (Input.GetKey(KeyCode.D) && !isMoving)
            StartCoroutine(MovePlayer(Vector3.right));

        fireTimer += Time.deltaTime;

        if (Input.GetKey(KeyCode.K) && !isMoving && boardManager.GetTileData(transform.position).type == "brown")
        {
            if (fireTimer >= refireRate)
            {
                fireTimer = 0;
                PlantSeed();
            }
        }
            
    }
   
    private IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;

        float elapsedTime = 0;

        originPos = transform.position;
        targetPos = originPos + direction;

        while(elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(originPos, targetPos, (elapsedTime / timeToMove));

           UpdateMapPosition();

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        isMoving = false;
    }

    private void UpdateMapPosition()
    {
        //this.GetTileData(transform.position);
    }

    private void GetTileData(Vector3 position)
    {
        //print("can burn:"+boardManager.GetTileData(position).canBurn);
    }

    private void PlantSeed()
    {
        print("SEME");
        boardManager.SetGreenTileData(transform.position);
    }
}