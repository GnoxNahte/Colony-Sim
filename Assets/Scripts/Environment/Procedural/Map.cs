using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    public Tilemap tilemap;
    public MapGenerationSettings settings;
    public MapGenerationPlayerSettings playerSettings { get { return map.settings.playerSettings; } }
    public Map map;

    [Range(0.001f, 0.5f)]
    public float scale;
    public Vector2Int offset;

    private Dictionary<Vector2, MapChunk> chunks;

    Player player;

    // Singleton
    public static Map instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            Debug.LogError("More than 1 Map. Destroying this. Name: " + name);
            return;
        }
        
        player = FindFirstObjectByType<Player>();
    }

    private void Start()
    {
        // 64 since 8x8 and power of 2. might increase more
        chunks = new Dictionary<Vector2, MapChunk>(64);
    }

    public void ClearFarChunks()
    {
        print("TODO: Clear chunks");
    }

    private void ClearAllChunks()
    {
        print("TODO: Clear all chunks");
    }

    public void Generate(Bounds bounds)
    {
        Vector2 bottomLeft = bounds.min;
        Vector2 topRight = bounds.max;
        print("Generating...");

    }

    // Clear and rebuild map
    public void Regenerate(Bounds bounds)
    {
        print("Regenerate map");
        ClearAllChunks();
        Generate(bounds);
    }
}