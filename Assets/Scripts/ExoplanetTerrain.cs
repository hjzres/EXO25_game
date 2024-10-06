using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TreeEditor;
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
    [Range(0, 1)] public float heightMultiplier;

    [Header("Perlin Maps")]
    public PerlinNoise[] noises = new PerlinNoise[2];

    [Header("Terrain Layers")]
    public TerrainLayer[] terrainLayers;

    [Button]
    public void GenerateTerrain()
    {
        ShaderProperties.SetValues();
        Terrain terrain = Terrain.activeTerrain;
        TerrainData data = terrain.terrainData;

        data.heightmapResolution = width + 1;
        data.size = new Vector3(width, 1000, length);

        // SET AMOUNT OF 2
        float[,] duneRegion = noises[0].Noise2D(width, length); 
        float[,] mountainRegion = noises[1].Noise2D(width, length);
        float[,] heights = new float[width, length];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                float blend = ((x - half) / period) + 0.5f;
                blend = Mathf.Clamp01(blend);

                heights[x, y] = Mathf.SmoothStep(duneRegion[x, y], mountainRegion[x, y], blend) * heightMultiplier;
            }
        }

        data.SetHeights(0, 0, heights);
        TextureTerrain(data, heights);
    }

    private void TextureTerrain(TerrainData data, float[,] heights)
    {
        float [,,] splatMap = new float[data.alphamapWidth, data.alphamapHeight, terrainLayers.Length];

        for (int x = 0; x < data.alphamapWidth; x++)
        {
            for (int y = 0; y < data.alphamapHeight; y++)
            {
                float steepness = data.GetSteepness((float)x / data.alphamapWidth, (float)y / data.alphamapHeight);
                float[] texturedWeights = new float[terrainLayers.Length];

                if (heights[x, y] < 0.4f && steepness < 20f)
                {
                    texturedWeights[0] = 1;
                }

                else if (steepness > 30f)
                {
                    texturedWeights[1] = 1;
                }

                float totalWeight = texturedWeights[0] + texturedWeights[1];
                
                for (int i = 0; i < terrainLayers.Length; i++)
                {
                    splatMap[x, y, i] = texturedWeights[i];
                }
            }
        }

        //data.SetAlphamaps(0, 0, splatMap);
    }

    [System.Serializable]
    public struct PerlinNoise
    {
        public float scale;
        public int octaves;
        public float lacunarity;
        public float persistance;
        [Range(0, 5)] public float heightMultiplier;

        public PerlinNoise(float scale = 1f, int octaves = 1, float lacunarity = 0.5f, float persistance = 2f, float heightMultiplier = 1f)
        {
            this.scale = scale;
            this.octaves = octaves;
            this.lacunarity = lacunarity;
            this.persistance = persistance;
            this.heightMultiplier = heightMultiplier;
        }

        public float[,] Noise2D(int width, int length)
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
                        amplitude *= persistance;

                        height += perlinValue;
                    }

                    noiseMap[x, y] = height * heightMultiplier;
                }
            }

            return noiseMap; 
        }
    }
}
