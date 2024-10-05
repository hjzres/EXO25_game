using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using NaughtyAttributes;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

// Attach script to a Game Manager Object
// Use static data
public class ExoplanetTerrain : MonoBehaviour
{
    [Header("Perlin Properties")]
    public int width = 256;
    public int height = 256;
    public float scale = 1f;
    public int octaves = 1;
    public float heightMultiplier = 1f;

    //[Header("Texture Properties")]
    

    [Button]
    public void GenerateTerrain()
    {
        Terrain terrain = Terrain.activeTerrain;
        TerrainData data = terrain.terrainData;

        data.heightmapResolution = width + 1;
        data.size = new Vector3(width, 100, height);
        
        data.SetHeights(0, 0, PerlinNoise2D(width, height, scale, octaves));
    }

    public void SetTerrainTextures(Terrain terrain)
    {
        TerrainLayer[] layers = terrain.terrainData.terrainLayers;
    }

    public float[,] PerlinNoise2D(float width, float length, float scale = 1f, int octaves = 1)
    {
        float[,] noiseMap = new float[(int)width, (int)length];

        Vector2[] octaveOffset = new Vector2[octaves];
        System.Random pseudoRNG = new System.Random();

        for (int i = 0; i < octaves; i++)
        {
            float xOffset = pseudoRNG.Next(-10000, 10000);
            float yOffset = pseudoRNG.Next(-10000, 10000);

            octaveOffset[i] = new Vector2(xOffset, yOffset);
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                float height = 0;

                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (float)x / width * scale + octaveOffset[i].x;
                    float sampleY = (float)y / length * scale + octaveOffset[i].y;
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                    height += perlinValue;
                }

                float multiplier = noiseMap[x, y] > 0.99f ? 0f : 1f; // finish multiplier

                noiseMap[x, y] = height * multiplier; // * heightMultiplier
            }
        }

        return noiseMap; 
    }
}
