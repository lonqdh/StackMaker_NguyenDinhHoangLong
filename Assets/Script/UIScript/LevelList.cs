using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="LevelData")]
public class LevelList : ScriptableObject
{
    public List<LevelItem> listLevels;
}
