using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridMovement : MonoBehaviour
{
    public BoardManager boardManager;
    
    private bool isMoving;
    //public float timeToMove = 0.25f;

    [SerializeField]
    private float plantSeedRate = 1f;
    private float plantSeedTimer = 0;

    public Animator animator;

    public float moveSpeed = 5f;
    public Transform movePoint;

    private bool isPlanting;
    public AudioSource hitTile;

    void Start()
    {
        isPlanting = false;
        animator.SetBool("isPlantingSeed", false);
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            isMoving = false;
            animator.SetBool("isMoving", false);
            animator.SetFloat("Horizontal", 0f);
            animator.SetFloat("Vertical", 0f);
        }

        //Movement
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f && !isMoving)
        {
            if (plantSeedTimer >= plantSeedRate && boardManager.GetTileDataIsWalkable(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f)))
            {
                movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                animator.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));

                animator.SetBool("isMoving", true);
                isMoving = true;
            }
        }
        else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f && !isMoving)
        {
            if (plantSeedTimer >= plantSeedRate && boardManager.GetTileDataIsWalkable(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f)))
            {
                movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                animator.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));

                animator.SetBool("isMoving", true);
                isMoving = true;
            }
        }

        //Plant Seed
        plantSeedTimer += Time.deltaTime;
        
        if (Input.GetKey(KeyCode.K) && !isMoving)
        {
            TileData tileData = boardManager.GetTileData(transform.position);
            if (tileData == null) return;

            string tileType = tileData.type;
            if (tileType != "brown") return;

            if (plantSeedTimer >= plantSeedRate)
            {
                plantSeedTimer = 0;
                PlantSeed();
            }
        }
        else
        {
            if (plantSeedTimer >= plantSeedRate && isPlanting)
            {
                boardManager.SetGreenTileData(transform.position);
                isPlanting = false;
                animator.SetBool("isPlantingSeed", isPlanting);
            }    
        } 
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
    }

    private void PlantSeed()
    {
        isPlanting = true;
        animator.SetBool("isPlantingSeed", isPlanting);
        MusicManager.Instance.PlaySingle(hitTile.clip);
    }
}