using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private Vector2 startMousePosition, endMousePosition;
    private bool isSwiping = false;

    private IEnumerator goCoroutine;
    private bool coroutineAllowed = true;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousePosition = Input.mousePosition;
            isSwiping = true;
        }

        if (isSwiping && Input.GetMouseButtonUp(0) && coroutineAllowed)
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
                        goCoroutine = Go(new Vector3(0f, 0f, 0.25f));
                        StartCoroutine(goCoroutine);
                    }
                    else
                    {
                        goCoroutine = Go(new Vector3(0f, 0f, -0.25f));
                        StartCoroutine(goCoroutine);
                    }
                }
                else
                {
                    if (swipeDirection.x > 0)
                    {
                        goCoroutine = Go(new Vector3(0.25f, 0f, 0f));
                        StartCoroutine(goCoroutine);
                    }
                    else
                    {
                        goCoroutine = Go(new Vector3(-0.25f, 0f, 0f));
                        StartCoroutine(goCoroutine);
                    }
                }
            }
        }
    }

    private IEnumerator Go(Vector3 direction)
    {
        coroutineAllowed = false;

        for (int i = 0; i <= 2; i++)
        {
            transform.Translate(direction);
            yield return new WaitForSeconds(0.01f);
        }

        for (int i = 0; i <= 2; i++)
        {
            transform.Translate(direction);
            yield return new WaitForSeconds(0.01f);
        }

        transform.Translate(direction);

        coroutineAllowed = true;
    }


    //[SerializeField] private Player player;
    //private Vector2 startPosition;
    //[SerializeField] private int swipePixelToDetect = 20;
    //private bool fingerDown;

    //private void Update()
    //{
    //    if (fingerDown == false && Input.GetMouseButton(0))
    //    {
    //        startPosition = Input.mousePosition;
    //        fingerDown = true;
    //    }

    //    if (fingerDown)
    //    {
    //        if (Input.mousePosition.y >= startPosition.y + swipePixelToDetect)
    //        {
    //            fingerDown = false;
    //            player.Move(Vector3.forward);
    //        }

    //        else if (Input.mousePosition.y <= startPosition.y - swipePixelToDetect)
    //        {
    //            fingerDown = false;
    //            player.Move(Vector3.back);
    //        }

    //        else if (Input.mousePosition.x >= startPosition.x + swipePixelToDetect)
    //        {
    //            fingerDown = false;
    //            player.Move(Vector3.right);

    //        }

    //        else if (Input.mousePosition.x <= startPosition.x - swipePixelToDetect)
    //        {
    //            fingerDown = false;
    //            player.Move(Vector3.left);

    //        }
    //    }
    //}





    //Testing
    //Mobile---
    //if (fingerDown == false && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
    //{
    //    startPosition = Input.touches[0].position;
    //    fingerDown = true;
    //}

    //if (fingerDown)
    //{
    //    if (Input.touches[0].position.y >= startPosition.y + swipePixelToDetect)
    //    {
    //        fingerDown = false;
    //        Debug.Log("Swipe up");
    //    }
    //    else if (Input.touches[0].position.x <= startPosition.x - swipePixelToDetect) 
    //    {
    //        fingerDown = false;
    //        Debug.Log("Swipe left");
    //    }
    //    else if(Input.touches[0].position.x >= startPosition.x - swipePixelToDetect)
    //    {
    //        fingerDown = false;
    //        Debug.Log("Swipe right");
    //    }
    //}

    //if (fingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
    //{
    //    fingerDown = false;
    //}

}
