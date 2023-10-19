using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapChunk : MonoBehaviour
{
    public Tilemap tilemap;
    public MapGenerationSettings settings => map.settings;
    public MapGenerationPlayerSettings playerSettings => map.settings.playerSettings;
    public Map map;

    [Range(0.001f, 0.5f)]
    public float scale;
    public Vector2Int offset;

    private void Start()
    {
        map = Map.instance;
    }

    [ContextMenu("Generate Map")]
    void GenerateMap()
    {
        double startTime = Time.realtimeSinceStartupAsDouble;
        settings.perlinNoiseScale = Mathf.Pow(scale, 1f / 3f);
        Vector2Int mapSize = playerSettings.size; // To shorten the code
        Vector2Int halfSize = mapSize / 2;
        BoundsInt bounds = new BoundsInt(
            -halfSize.x, -halfSize.y, 0, // Position
            mapSize.x, mapSize.y, 1      // Size
        );

        // Generate the array
        TileBase[] tileArray = new TileBase[mapSize.x * mapSize.y];
        for (int x = 0; x < mapSize.x; ++x)
        {
            for (int y = 0; y < mapSize.y; ++y)
            {
                int index = x * mapSize.x + y;
                //tileArray[index] = settings.tiles[index % 2].tile;
                float value = Mathf.PerlinNoise(x * settings.perlinNoiseScale, y * settings.perlinNoiseScale);
                tileArray[x * mapSize.x + y] = value < 0.5 ? settings.tiles[0].tile : settings.tiles[1].tile;
            }
        }

        tilemap.SetTilesBlock(bounds, tileArray);
        print("Time taken: " + (Time.realtimeSinceStartupAsDouble - startTime));
    }

    [BurstCompile]
    struct MapGenerationJob : IJobParallelFor
    {
        public Vector2Int size;
        public float noiseScale;
        public Vector2Int offset;
        public NativeArray<ushort> mapChunk;

        [BurstCompile]
        public void Execute(int index)
        {
            float value = noise.snoise(new float2(index % size.x + offset.x, index / size.y + offset.y) * noiseScale);
            //Unity.Mathematics.Random rand = new Unity.Mathematics.Random();
            if (value < 0.5f)
                mapChunk[index] = 0;
            else
                mapChunk[index] = 1;
        }
    }

    [ContextMenu("Generate Map Job")]
    void GenerateMapJob()
    {
        double startTime = Time.realtimeSinceStartupAsDouble;
        settings.perlinNoiseScale = scale;
        Vector2Int mapSize = playerSettings.size; // To shorten the code
        Vector2Int halfSize = mapSize / 2;
        BoundsInt bounds = new BoundsInt(
            -halfSize.x, -halfSize.y, 0, // Position
            mapSize.x, mapSize.y, 1      // Size
        );

        var array = new NativeArray<ushort>(mapSize.x * mapSize.y, Allocator.TempJob);
        // Init Job
        var job = new MapGenerationJob()
        {
            mapChunk = array,
            size = mapSize,
            noiseScale = scale,
            offset = offset,
        };
        JobHandle handle = job.Schedule(array.Length, 32);
        handle.Complete();

        var tileArray = new TileBase[mapSize.x * mapSize.y];
        for (int x = 0; x < mapSize.x; ++x)
        {
            for (int y = 0; y < mapSize.y; ++y)
            {
                int index = x * mapSize.x + y;
                tileArray[index] = settings.tiles[array[index]].tile;
            }
        }
        array.Dispose();

        tilemap.SetTilesBlock(bounds, tileArray);
        print("Time taken: " + (Time.realtimeSinceStartupAsDouble - startTime));

    }

    [ContextMenu("Clear Map")]
    void ClearMap()
    {
        tilemap.ClearAllTiles();
    }

    [ContextMenu("Clear Editor Map")]
    void ClearEditorMap()
    {
        tilemap.ClearAllEditorPreviewTiles();
    }

    private string GenerateMapHash(Vector2 pos)
    {
        return $"{pos.x},{pos.y}";
    }

    private void OnValidate()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode || EditorApplication.isUpdating)
            return;

        ClearMap();
        GenerateMapJob();
    }
}
