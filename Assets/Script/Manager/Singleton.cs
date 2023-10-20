using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
            }
            if (instance == null)
            {
                instance = new GameObject().AddComponent<T>();
            }
            return instance;
        }
    }

    //private static T instance;
    //public static T Instance { get; private set; }

    //protected virtual void Awake()
    //{
    //    if (instance != null)
    //    {
    //        Destroy(this); //Or GameObject as appropriate
    //        return;
    //    }
    //    instance = this;
    //}
}
