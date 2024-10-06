using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShaderProperties 
{
    // Skybox Properties
    // SET TO WEBSITE VALUES, THESE WILL AUTOMATICALLY CHANGE VALUES IN UNITY
    public static Color skyColor; // How bright the sun is based on the color light it emits, sky color is supposed to be black though
    public static Color groundColor; // Determined by material of the planet
    public static float starSize; //Size of the sun
    public static int planetTemp; //Adds a tint to the planet depending on how hot or cold our planet is
    public static int starDistance; //Increases or decreases the size of the sun
    public static Color horizonColor; // Not needed
    public static float skyBlend; // Not needed
    public static float groundBlend; //Not needed
    public static float horizonBlend; //Not needed
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
