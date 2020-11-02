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
    private float plantSeedRate = 1f;
    private float plantSeedTimer = 0;

    public Animator animator;

    public float moveSpeed = 5f;
    public Transform movePoint;

    void Start()
    {
        animator.SetBool("isPlantingSeed", false);
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            animator.SetBool("isMoving", false);
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if (plantSeedTimer >= plantSeedRate)
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                    animator.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));

                    animator.SetBool("isMoving", true);
                }
            }
            else
            {
                animator.SetFloat("Horizontal", 0f);
            }

            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (plantSeedTimer >= plantSeedRate)
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                    animator.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));

                    animator.SetBool("isMoving", true);
                }
            }
            else
            {
                animator.SetFloat("Vertical", 0f);
            }
        }
        

        //old move code
        /*if (Input.GetKey(KeyCode.W) && !isMoving)
        {
            StartCoroutine(MovePlayer(Vector3.up));
        }
        else if (Input.GetKey(KeyCode.A) && !isMoving)
        {
            StartCoroutine(MovePlayer(Vector3.left));
        }
        else if (Input.GetKey(KeyCode.S) && !isMoving)
        {
            StartCoroutine(MovePlayer(Vector3.down));
        }
        else if (Input.GetKey(KeyCode.D) && !isMoving)
        {
            StartCoroutine(MovePlayer(Vector3.right));
        }*/
            

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
            if (plantSeedTimer >= plantSeedRate)
                animator.SetBool("isPlantingSeed", false);
        }
       
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
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

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        isMoving = false;
    }

    private void PlantSeed()
    {
        animator.SetBool("isPlantingSeed", true);
        //print("SEME");
        boardManager.SetGreenTileData(transform.position);
    }
}