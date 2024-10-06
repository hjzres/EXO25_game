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
    public int height = 256;
    public float scale = 1f;
    public int octaves = 1;
    public float[,] noiseMap;

    [Header("Texture Properties")]
    private TerrainLayer[] layers;

    [Button]
    public void GenerateTerrain()
    {
        Terrain terrain = Terrain.activeTerrain;
        TerrainData data = terrain.terrainData;
        noiseMap = PerlinNoise2D(width, height, scale, octaves);

        data.heightmapResolution = width + 1;
        data.size = new Vector3(width, 100, height);
        
        data.SetHeights(0, 0, noiseMap);
    }

    public void SetTerrainTextures(Terrain terrain)
    {
        layers = terrain.terrainData.terrainLayers;

        float[,] heights = terrain.terrainData.GetHeights(0, 0, width, height);

        for (int i = 0; i < heights.GetLength(0); i++)
        {
            for (int j = 0; j < heights.GetLength(1); j++)
            {
                if (height < 10)
                {

                }

                else
                {

                }
            }
        }
    }

    public float[,] PerlinNoise2D(float width, float length, float scale = 1f, int octaves = 1)
    {
        float[,] noiseMap = new float[(int)width, (int)length];

        int seed = Random.Range(-100000, 100000);
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
                float height = 0;

                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (float)x / width * scale + octaveOffset[i].x;
                    float sampleY = (float)y / length * scale + octaveOffset[i].y;
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                    height += perlinValue;
                }

                float multiplier = height < 0.3f ? 0.5f : (height >= 0.3f || height <= 0.9f ? 1f : 2.3f);
                height *= multiplier;
                noiseMap[x, y] = height;
            }
        }

        return noiseMap; 
    }
}
