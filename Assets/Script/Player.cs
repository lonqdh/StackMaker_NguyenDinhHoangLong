using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public List<Brick> brickList = new List<Brick>();
    //[SerializeField] Brick spawnPoint;
    [SerializeField] private float speed = 5f;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask brickLayer;
    [SerializeField] private LayerMask unbrickLayer;
    private Rigidbody rb;


    //Swipe and Movement
    private Vector2 startMousePosition, endMousePosition;
    private bool isSwiping = false;
    private Vector3 targetPosition;
    private bool isMoving;
    private Vector3 direction;

    //CollectBrick
    private Brick brick;
    private int currentColor;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        Debug.DrawRay(transform.position + new Vector3(0f, 0f, 0.5f), Vector3.down, Color.red);

        if (GameManager.Instance.IsState(GameState.Gameplay) && !isMoving)
        {
            CheckSwipe();
        }

        else if (isMoving)
        {
            MovingTowards();
            CollectBricks();
            DistributeBricks();
        }
    }

    public void OnInit()
    {
        isMoving = false;
    }

    private void CheckSwipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousePosition = Input.mousePosition;
            isSwiping = true;
        }

        if (isSwiping && Input.GetMouseButtonUp(0))
        {
            endMousePosition = Input.mousePosition;
            isSwiping = false;

            Vector2 swipeDirection = endMousePosition - startMousePosition;

            if (swipeDirection.magnitude > 10f)
            {
                if (Mathf.Abs(swipeDirection.y) > Mathf.Abs(swipeDirection.x))
                {
                    if (swipeDirection.y > 0)
                    {
                        Debug.Log("Forward");
                        this.direction = Vector3.forward;
                        targetPosition = EndPoint();
                        isMoving = true;

                    }
                    else if (swipeDirection.y < 0)
                    {
                        Debug.Log("Backward");
                        this.direction = Vector3.back;
                        targetPosition = EndPoint();
                        isMoving = true;

                    }
                }
                else
                {
                    if (swipeDirection.x > 0)
                    {
                        Debug.Log("Right");
                        this.direction = Vector3.right;
                        targetPosition = EndPoint();
                        isMoving = true;

                    }
                    else if (swipeDirection.x < 0)
                    {
                        Debug.Log("Left");
                        this.direction = Vector3.left;
                        targetPosition = EndPoint();
                        isMoving = true;

                    }
                }
            }
        }
    }

    private void MovingTowards()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            isMoving = false;
        }
    }

    private Vector3 EndPoint()
    {
        RaycastHit rayHit;
        Debug.DrawLine(transform.position, transform.position + direction * 50f, Color.red);
        bool hitTarget = Physics.Raycast(transform.position, direction, out rayHit, 100f, wallLayer);
        if (hitTarget)
        {
            Vector3 endPos = new Vector3(rayHit.point.x - direction.x * 0.5f, transform.position.y, rayHit.point.z - direction.z * 0.5f);
            return endPos;
        }

        return Vector3.zero;
    }

    private void CollectBricks()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector2.down, out hit, 5f, brickLayer))
        {
            brick = hit.collider.gameObject.GetComponent<Brick>();
            
            if (brickList.Count == 0)
            {
                currentColor = (int)brick.color;
            }

            if(currentColor == (int)brick.color)
            {
                brick.GetComponent<Collider>().enabled = true;
                Vector3 stackPosition = new Vector3(transform.position.x, transform.position.y + 1f + brickList.Count * 0.36f, transform.position.z);
                brick.transform.position = stackPosition;
                brick.transform.SetParent(transform);
                brickList.Add(brick);
            }
        }
    }

    private void DistributeBricks()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position /*+ new Vector3(0f, 0f, 0.25f)*/, Vector3.down, out hit, 5f, unbrickLayer))
        {
            //brick = hit.collider.gameObject.GetComponent<Brick>();
            if (brickList.Count > 0)
            {
                brickList[brickList.Count - 1].GetComponent<Collider>().isTrigger = false;
                brickList[brickList.Count - 1].gameObject.layer = 0; //phai co neu khong thi brick dc distribute van co layer la brickLayer nen player se lai nhat
                brickList[brickList.Count - 1].transform.parent = null;
                //brickList[brickList.Count - 1].transform.SetParent(null);
                brickList[brickList.Count - 1].transform.position = hit.transform.position;

                brickList.RemoveAt(brickList.Count - 1); //remove the last item that was added into the list ( if the list total items is above 0 else the system will throw IndexOutOfRangeException
                Destroy(hit.collider.gameObject);
                //hit.collider.gameObject.SetActive(false);
            }
            else
            {
                isMoving = false;
                rb.velocity = Vector3.zero;
            }
        }
    }


}
