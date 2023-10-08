using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Map Generation/Player Settings")]
public class MapGenerationPlayerSettings : ScriptableObject
{
    public string seed;

    public Vector2Int size;

}