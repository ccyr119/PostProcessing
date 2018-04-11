using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent, ExecuteInEditMode]
[AddComponentMenu("ImageEffect/Camera Dynamic Resolution", 2000)]
[RequireComponent(typeof(Camera))]
public class DynResEffect : MonoBehaviour {

    [Range(0.1f, 1.0f)]
    public float m_RTScaleFactor = 0.7f;
    
    Camera m_Camera;
    RenderTexture targetRenderTexutre;
    int m_renderTargetWidth = 0;
    int m_renderTargetHeight = 0;
    float m_CurRTScaleFactor;
    int m_CurPixelWidth;
    int m_CurPixelHeight;
    
    void OnEnable()
    {
        m_Camera = GetComponent<Camera>();
    }

    private void CalculateRT()
    {
        if (m_CurRTScaleFactor != m_RTScaleFactor || m_CurPixelWidth != m_Camera.pixelWidth || m_CurPixelHeight != m_Camera.pixelHeight)
        {
            m_RTScaleFactor = Mathf.Clamp(m_RTScaleFactor, 0.1f, 1.0f);
            m_CurRTScaleFactor = m_RTScaleFactor;
            if (null != targetRenderTexutre)
            {
                RenderTexture.ReleaseTemporary(targetRenderTexutre);
                targetRenderTexutre = null;
            }
            if (1.0f != m_CurRTScaleFactor)
            {
                m_CurPixelWidth = m_Camera.pixelWidth;
                m_CurPixelHeight = m_Camera.pixelHeight;
                m_renderTargetWidth = Mathf.FloorToInt(m_CurPixelWidth * m_CurRTScaleFactor);
                m_renderTargetHeight = Mathf.FloorToInt(m_CurPixelHeight * m_CurRTScaleFactor);
                targetRenderTexutre = RenderTexture.GetTemporary(m_renderTargetWidth, m_renderTargetHeight, 24);
            }
        }
    }

    void OnPreCull()
    {
        CalculateRT();
        m_Camera.targetTexture = targetRenderTexutre;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        RenderTexture dest = null;
        if (m_Camera.targetTexture != destination)        
            dest = destination;

        m_Camera.targetTexture = null;
        Graphics.Blit(source, dest);
    }

    void OnDestroy()
    {
        if (null != targetRenderTexutre)
        {
            RenderTexture.ReleaseTemporary(targetRenderTexutre);
            targetRenderTexutre = null;
        }
    }
}
