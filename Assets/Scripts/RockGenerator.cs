using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class RockGenerator : MonoBehaviour
{
    public Terrain terrain;
    public GameObject[] rocks;
    public List<GameObject> cubes = new List<GameObject>();
    public float[,] perlinValues;
    [Min(1)] public int maxValue;
    


    [Button]
    public void TestTerrain()
    {


        if (cubes.Count > 0)
        {
            foreach (GameObject cube in cubes)
            {
               DestroyImmediate(cube);
            }

            cubes.Clear();
        }
        
        int width = (int)terrain.terrainData.size.x;
        int length = (int)terrain.terrainData.size.z;
        int peak = (int)terrain.terrainData.size.y;  
        
        //RaycastHit hit;
        //Vector3 slopeDirection;
/*
        for (int i = 0; i<=width; i+=250){
            for (int j = 0; j<=length; j+=250){
                int randRock = Random.Range(0,10);
                float yValue = terrain.SampleHeight(new Vector3(i, terrain.terrainData.GetHeight(i, j), j));
                //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                GameObject rock;
                if (randRock >=7){
                    rock = Instantiate(rocks[0], new Vector3(i,yValue,j), Quaternion.identity);
                }
                else{
                    rock = Instantiate(rocks[1], new Vector3(i,yValue,j), Quaternion.identity);
                }
                cubes.Add(rock); 
                //cube.transform.position = new Vector3(i, yValue,j);

                //Vector3 normal = terrain.terrainData.GetInterpolatedNormal(i, j);
                bool raycast = Physics.Raycast(rock.transform.position, -rock.transform.up, out hit, 1f);
                Vector3 _dir = new Vector3(i,yValue,j);
                _dir = transform.up;
                if (raycast)  
                {
                    Vector3 tangent = Vector3.ProjectOnPlane( _dir, hit.normal );
			        rock.transform.rotation = Quaternion.LookRotation( hit.normal  , tangent);
                    //Quaternion rotation = Quaternion.FromToRotation(rock.transform.up, hit.point);
                    //rock.transform.rotation = rotation;
                }

                //cube.transform.localScale = new Vector3(10,10,10);
                //cubes.Add(cube);
            }
        }
*/
        perlinValues = new float[width, length];
        for (int x = 0; x < width; x+=10)
        {
            for (int y = 0; y < length; y+=10)
            {
                float sampleX = (float)x / width * 5f;
                float sampleY = (float)y / width * 5f;

                perlinValues[x, y] = Mathf.PerlinNoise(sampleX, sampleY) * 3 - 1;
            }
        }

        for (int i = 0; i < perlinValues.GetLength(0); i++)
        {
            for (int j = 0; j < perlinValues.GetLength(1); j++)
            {
                GameObject rock;
                if (perlinValues[i, j] >= 0.3f)
                {
                    int randRock = Random.Range(0,100);
                    int randSpawn = Random.Range(1,maxValue);
                    float yValue = terrain.SampleHeight(new Vector3(i, terrain.terrainData.GetHeight(i, j), j));
                    
                    if (randRock <=10 && randSpawn <2){
                        rock = Instantiate(rocks[0], new Vector3(i,yValue,j), Quaternion.identity);
                        rock.transform.rotation = Quaternion.Euler(0,Random.Range(0,360),0);
                        cubes.Add(rock); 
                    }

                    else if (randRock >10 && randSpawn <2){
                        rock = Instantiate(rocks[1], new Vector3(i,yValue,j), Quaternion.identity);
                        rock.transform.rotation = Quaternion.Euler(0,Random.Range(0,360),0);
                        cubes.Add(rock); 
                    }
                }
            }
        }
    }

    [Button]
    public void ClearList(){
        foreach (GameObject rock in cubes){
            DestroyImmediate(rock);
        }

        cubes.Clear();
    }
        
}
