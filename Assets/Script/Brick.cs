using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public enum BrickColor
    {
        Blue = 1,
        Yellow = 2,
        Green = 3,
    }
    [SerializeField] public BrickColor color;

}


