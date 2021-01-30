using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Snow : MonoBehaviour
{
    [SerializeField]
    private RenderTexture renderTexture;

    [SerializeField]
    private Material m_defaultSnowClear;

    [SerializeField]
    private float m_startSnowHeight = 1.0f;

    private Color GetColor(float alpha)
    {
        return new Color(0.5f, 0.5f, 1.0f, alpha);
    }

    void Start()
    {
        if (renderTexture != null && m_defaultSnowClear != null)
        {
            m_defaultSnowClear.color = GetColor(m_startSnowHeight);
            Graphics.Blit(null, renderTexture, m_defaultSnowClear);
        }
        else
        {
            Debug.LogWarning("Add RenderTexture or Material!");
            enabled = false;
        }
    }
}
