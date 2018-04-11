using UnityEngine;
using System.Collections;

[DisallowMultipleComponent, ExecuteInEditMode, ImageEffectAllowedInSceneView]
[AddComponentMenu("ImageEffect/CRT", 2000)]
public class CRTEffect : ImageEffectBase
{
    public Material material;

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {        
        Graphics.Blit(source, RenderDestination(source, destination), material);
    }
}