using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class RockGenerator : MonoBehaviour
{
    [Header("Properties")]
    public GameObject[] rocks;
    public List<GameObject> cubes = new List<GameObject>();
    public float[,] perlinValues;
    [Min(1)] public int maxValue;

    public void PlaceRocks(int width, int length)
    {
        Terrain terrain = Terrain.activeTerrain;

        if (cubes.Count > 0)
        {
            foreach (GameObject cube in cubes)
            {
               DestroyImmediate(cube);
            }

            DestroyImmediate(GameObject.Find("Rock Parent"));
            cubes.Clear();
        }
        
        perlinValues = new float[width, length];
        for (int x = 0; x < width; x += 10)
        {
            for (int y = 0; y < length; y += 10)
            {
                float sampleX = (float)x / width * 5f;
                float sampleY = (float)y / width * 5f;

                perlinValues[x, y] = Mathf.PerlinNoise(sampleX, sampleY) * 3 - 1;
            }
        }

        GameObject rockParent = new GameObject("Rock Parent");

        for (int i = 0; i < perlinValues.GetLength(0); i++)
        {
            for (int j = 0; j < perlinValues.GetLength(1); j++)
            {
                GameObject rock;
                if (perlinValues[i, j] >= 0.3f)
                {
                    int randRock = Random.Range(0, 100);
                    int randSpawn = Random.Range(1, maxValue);
                    float yValue = terrain.SampleHeight(new Vector3(i, terrain.terrainData.GetHeight(i, j), j));
                    
                    if (randRock <= 10 && randSpawn < 2){
                        rock = Instantiate(rocks[0], new Vector3(i,yValue,j), Quaternion.identity);
                        rock.transform.rotation = Quaternion.Euler(0,Random.Range(0,360),0);
                        rock.transform.parent = rockParent.transform;
                        cubes.Add(rock); 
                    }

                    else if (randRock > 10 && randSpawn < 2){
                        rock = Instantiate(rocks[1], new Vector3(i, yValue, j), Quaternion.identity);
                        rock.transform.rotation = Quaternion.Euler(0, Random.Range(0,360), 0);
                        rock.transform.parent = rockParent.transform;
                        cubes.Add(rock); 
                    }
                }
            }
        }
    }

    [Button]
    public void ClearList()
    {
        foreach (GameObject rock in cubes)
        {
            DestroyImmediate(rock);
        }

        DestroyImmediate(GameObject.Find("Rock Parent"));
        cubes.Clear();
    }
        
}
