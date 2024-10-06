using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShaderProperties 
{
    // Skybox Properties
    // SET TO WEBSITE VALUES, THESE WILL AUTOMATICALLY CHANGE VALUES IN UNITY
    public static Color skyColor;
    public static Color groundColor;
    public static Color horizonColor;
    public static float skyBlend;
    public static float groundBlend;
    public static float horizonBlend;
    public static Material skyboxMaterial;

    public static void SetValues()
    {
        // Add website values above
        skyboxMaterial = RenderSettings.skybox;
        skyboxMaterial.SetColor("Sky Colour", skyColor);
        skyboxMaterial.SetColor("Ground Colour", groundColor);
        skyboxMaterial.SetColor("Horizon Colour", horizonColor);
        skyboxMaterial.SetFloat("Sky Blend", skyBlend);
        skyboxMaterial.SetFloat("Ground Blend", groundBlend);
        skyboxMaterial.SetFloat("Horizon Blend", horizonBlend);
    }  
}
