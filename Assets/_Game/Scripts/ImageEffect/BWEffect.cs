using UnityEngine;
using System.Collections;

[DisallowMultipleComponent, ExecuteInEditMode, ImageEffectAllowedInSceneView]
[AddComponentMenu("ImageEffect/Black White", 2000)]
public class BWEffect : ImageEffectBase
{
    [Range(0f,1f)]
    public float intensity;
    private Material material;

    // Creates a private material used to the effect
    void Awake()
    {
        material = new Material(Shader.Find("Hidden/BWDiffuse"));
    }

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        RenderTexture dest = RenderDestination(source, destination);        

        if (intensity == 0)
        {
            Graphics.Blit(source, dest);
            return;
        }

        material.SetFloat("_bwBlend", intensity);
        Graphics.Blit(source, dest, material);
    }
}