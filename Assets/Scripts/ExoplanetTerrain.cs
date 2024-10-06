using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

// Attach script to a Game Manager Object
// Use static data
public class ExoplanetTerrain : MonoBehaviour
{
    [Header("Perlin Properties")]
    public int width = 256;
    public int length = 256;
    public float period = 1.0f;
    public int half = 500;

    [Button]
    public void GenerateTerrain()
    {
        Terrain terrain = Terrain.activeTerrain;
        TerrainData data = terrain.terrainData;

        data.heightmapResolution = width + 1;
        data.size = new Vector3(width, 100, length);
        
        float[,] duneRegion = PerlinNoise2D(5f, 1);
        float[,] mountainRegion = PerlinNoise2D(20f, 1, 0.5f, 5f);
        float[,] heights = new float[width, length];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                float blend = ((x - half) / period) + 0.5f;
                blend = Mathf.Clamp01(blend);

                heights[x, y] = Mathf.SmoothStep(duneRegion[x, y], mountainRegion[x, y], blend);
            }
        }

        data.SetHeights(0, 0, heights);
    }

    public float[,] PerlinNoise2D(float scale = 1f, int octaves = 1, float lacunarity = 0.5f, float persistence = 2f)
    {
        float[,] noiseMap = new float[width, length];

        int seed = UnityEngine.Random.Range(-100000, 100000);
        Vector2[] octaveOffset = new Vector2[octaves];
        System.Random pseudoRNG = new System.Random(seed);

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
                float frequency = 1f;
                float amplitude = 1f;
                float height = 0;

                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (float)x / width * scale * frequency + octaveOffset[i].x;
                    float sampleY = (float)y / length * scale * frequency + octaveOffset[i].y;
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * amplitude;

                    frequency *= lacunarity;
                    amplitude *= persistence;

                    height += perlinValue;
                }

                noiseMap[x, y] = height;
            }
        }

        return noiseMap; 
    }
}
