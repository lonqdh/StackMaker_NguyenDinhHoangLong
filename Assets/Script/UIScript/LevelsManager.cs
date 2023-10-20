using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    public static LevelsManager Instance;
    private GameObject currentLevel;


    private void Start()
    {
        Instance = this;
        //player.transform.position = 
    }

    public void SpawnLevel()
    {
        currentLevel = Resources.Load<GameObject>("Level1");
        Instantiate(currentLevel);
    }

}


//public class LevelsManagerTest : MonoBehaviour
//{
//    public static LevelsManagerTest Instance;

//    private void Start()
//    {
//        Instance = this;
//    }

//    public void SpawnLevel()
//    {
//        Debug.Log("Ok");
//    }
//}
