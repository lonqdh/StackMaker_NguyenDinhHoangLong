using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    [SerializeField] private GameObject startPoint;
    [SerializeField] private GameObject finishPoint;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject victoryMenu;
    private float setDistance = 0.1f;

    private void Start()
    {
        player.transform.position = startPoint.transform.position;
    }

    private void Update()
    {
        finishLevel();
    }

    private void finishLevel()
    {
        if (player != null && startPoint != null && finishPoint != null)
        {
            if (Vector3.Distance(player.transform.position, finishPoint.transform.position) < setDistance)
            {
                victoryMenu.SetActive(true);
            }
        }
    }



}
