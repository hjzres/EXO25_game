using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class EnvironmentManager : MonoBehaviour
{
    [Header("References")]
    public Camera cam;
    public Terrain terrain;
    public Light directionalLight;
    public Volume globalVolume;

    private void Start()
    {
        ShaderProperties.SetValues();
        cam = FindObjectOfType<Camera>();
        terrain = Terrain.activeTerrain;
        directionalLight = FindObjectOfType<Light>();
        globalVolume = FindObjectOfType<Volume>();
    }    

    private void Update()
    {
        SetLightValues();
        SetParameters();
    }

    private void SetLightValues()
    {
        RenderSettings.fogColor = ShaderProperties.skyColor;
    }

    private void SetParameters()
    {

    }
}
