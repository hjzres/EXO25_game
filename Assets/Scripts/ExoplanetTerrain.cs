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

    [Header("Perlin Maps")]
    public PerlinNoise[] noises = new PerlinNoise[2];

    [Header("Terrain Layers")]
    public TerrainLayer[] terrainLayers;
    private RockGenerator rockGeneratorScript;

    public void Start()
    {
        ShaderProperties.SetValues();
        Terrain terrain = Terrain.activeTerrain;
        TerrainData data = terrain.terrainData;
        rockGeneratorScript = GetComponent<RockGenerator>();

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

                heights[x, y] = Mathf.SmoothStep(duneRegion[x, y], mountainRegion[x, y], blend) * 0.1f * ShaderProperties.heightMultiplier;
            }
        }

        data.SetHeights(0, 0, heights);
        rockGeneratorScript.PlaceRocks(width, half - 20);
    }

    [System.Serializable]
    public struct PerlinNoise
    {
        public float scale;
        public int octaves;
        public float frequency;
        public float amplitude;
        public float lacunarity;
        public float persistance;
        [Range(0, 5)] public float heightMultiplier;

        public PerlinNoise(float scale = 1f, int octaves = 1, float frequency = 1f, float amplitude = 1f, float lacunarity = 0.5f, float persistance = 0.5f, float heightMultiplier = 1f)
        {
            this.scale = scale;
            this.octaves = octaves;
            this.frequency = frequency;
            this.amplitude = amplitude;
            this.lacunarity = lacunarity;
            this.persistance = persistance;
            this.heightMultiplier = heightMultiplier;
        }

        public float[,] Noise2D(int width, int length)
        {
            float[,] noiseMap = new float[width, length];
            float newScale = scale * ShaderProperties.noiseScaleGlobal * 0.1f;

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
                    float frequency = this.frequency * ShaderProperties.frequency * 0.75f;
                    float amplitude = this.amplitude * ShaderProperties.amplitude * 0.75f;
                    float height = 0;

                    for (int i = 0; i < octaves; i++)
                    {
                        float sampleX = (float)x / width * newScale * frequency + octaveOffset[i].x;
                        float sampleY = (float)y / length * newScale * frequency + octaveOffset[i].y;
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
