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

    public Animator animator;

    public float moveSpeed = 5f;
    public Transform movePoint;

    void Start()
    {
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
                movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                animator.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));

                animator.SetBool("isMoving", true);
            }

            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                animator.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));

                animator.SetBool("isMoving", true);
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
        print("SEME");
        boardManager.SetGreenTileData(transform.position);
    }
}