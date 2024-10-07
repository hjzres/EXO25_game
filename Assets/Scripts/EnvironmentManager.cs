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

    private Color sunColor;

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
        string type = ShaderProperties.starType;

        if (type == "O")
        {
            sunColor = new Color(167, 232, 235, 1) / 255;
        }
        
        else if (type == "B")
        {
            sunColor = new Color(220, 251, 252, 1) / 255;   
        }

        else if (type == "A")
        {
            sunColor = new Color(255, 255, 255 / 1) / 255;
        }

        else if (type == "F")
        {
            sunColor = new Color(255, 234, 133, 1) / 255; 
        }

        else if (type == "G")
        {
            sunColor = new Color(255, 217, 57, 1) / 255;
        }

        else if (type == "K")
        {
            sunColor = new Color(235, 159, 27, 1) / 255;
        }

        else if (type == "M")
        {
            sunColor = new Color(255, 0, 0) / 255;
        }

        ShaderProperties.skyboxMaterial.SetColor("_Sun_Colour", sunColor);
        ShaderProperties.skyboxMaterial.SetFloat("_Sun_Size", ShaderProperties.starSize);
    }
}
