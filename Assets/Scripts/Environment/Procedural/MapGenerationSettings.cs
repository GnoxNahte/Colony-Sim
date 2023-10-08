using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Map Generation/Settings")]
public class MapGenerationSettings : ScriptableObject
{
    [Serializable]
    public class TileData
    {
        public TileBase tile;
    }
    public MapGenerationPlayerSettings playerSettings;

    public TileData[] tiles;
    public float perlinNoiseScale;
}
