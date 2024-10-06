using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [Header("References")]
    public Camera cam;
    public Terrain terrain;
    public Light directionalLight;

    private void Start()
    {
        ShaderProperties.SetValues();
        cam = FindObjectOfType<Camera>();
        terrain = Terrain.activeTerrain;
        directionalLight = FindObjectOfType<Light>();
    }    

    private void Update()
    {
        SetLightValues();
        SetPostProcessingValues();
    }

    private void SetLightValues()
    {

    }

    private void SetPostProcessingValues()
    {

    }
}
