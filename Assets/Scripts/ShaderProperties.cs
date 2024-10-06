using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShaderProperties 
{
    // Skybox Properties
    // SET TO WEBSITE VALUES, THESE WILL AUTOMATICALLY CHANGE VALUES IN UNITY
    public static Color skyColor; // How bright the sun is based on the color light it emits, sky color is supposed to be black though
    public static Color groundColor; // Determined by material of the planet
    public static Color horizonColor; 
    public static Color sunColor;
    public static float starSize; //Size of the sun
    public static int planetTemp; //Adds a tint to the planet depending on how hot or cold our planet is
    public static int starDistance; //Increases or decreases the size of the sun
    public static Material skyboxMaterial;


    public static void SetValues()
    {
        // Add website values above
        skyboxMaterial = RenderSettings.skybox;
        skyColor = skyboxMaterial.GetColor("_Sky_Colour");
        horizonColor = skyboxMaterial.GetColor("_Horizon_Colour");
        groundColor = skyboxMaterial.GetColor("_Ground_Colour");
        sunColor = skyboxMaterial.GetColor("_Sun_Colour");
    }  
}
