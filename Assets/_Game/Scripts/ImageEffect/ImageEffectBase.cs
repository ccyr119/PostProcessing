using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public abstract class ImageEffectBase : MonoBehaviour
{
    Camera m_Camera;
    private void OnEnable()
    {
        m_Camera = GetComponent<Camera>();
    }

    protected RenderTexture RenderDestination(RenderTexture source, RenderTexture destination)
    {
        if (null == destination
#if UNITY_EDITOR
            || m_Camera.cameraType == CameraType.SceneView
#endif
            )
            return destination;

        RenderTexture dest = null;
        if (source.name.Equals(destination.name))
            dest = destination;
        return dest;
    }
}