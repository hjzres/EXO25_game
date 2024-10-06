using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JavascriptHook : MonoBehaviour
{
    public void StarDistance(float distance)
    {
        ShaderProperties.starDistance = distance;
    } 

    public void PlanetColour(string hexColor)
    {
        ColorUtility.TryParseHtmlString(hexColor, out ShaderProperties.planetColor);
    }

    public void HeightMultiplier(float multiplier)
    {
        ShaderProperties.heightMultiplier = multiplier;
    }

    public void Frequency(float frequency)
    {
        ShaderProperties.frequency = frequency;
    }

    public void Amplitude(float amplitude)
    {
        ShaderProperties.amplitude = amplitude;
    }

    public void NoiseScaleDune(float scale)
    {
        ShaderProperties.noiseScale = scale;
    }
}
