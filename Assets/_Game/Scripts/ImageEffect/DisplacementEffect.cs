using UnityEngine;
using System.Collections;

[DisallowMultipleComponent, ExecuteInEditMode, ImageEffectAllowedInSceneView]
[AddComponentMenu("ImageEffect/Displacement", 2000)]
public class DisplacementEffect : ImageEffectBase
{
    [Range(0f, 1f)]
    public float m_Strength;
    public Material material;
    
    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_Strength", m_Strength);       
        Graphics.Blit(source, RenderDestination(source, destination), material);        
    }
}