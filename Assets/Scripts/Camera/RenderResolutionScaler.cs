using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderResolutionScaler : MonoBehaviour
{
    public RectTransform rectTransform;
    public Camera mainCamera;
    [Range(0.125f,1f)]
    public float defaultScale = 0.5f;
    public FilterMode defaultFilterMode;
    public RawImage image;

    private RenderTexture texture;
    private float currentScale;
    private FilterMode currentFilterMode;

    private void Start()
    {
        ApplyResolutionScale(defaultScale, defaultFilterMode);
    }

    public void ApplyResolutionScale(float scale)
    {
        ApplyResolutionScale(scale, currentFilterMode);
    }

    public void ApplyFilterMode(FilterMode filterMode)
    {
        ApplyResolutionScale(currentScale, filterMode);
    }

    public void ApplyResolutionScale(float scale,FilterMode filterMode)
    {
        if (scale == 0)
            scale = defaultScale;

        image.color = Color.white;

        if (texture)
        {
            texture.DiscardContents();
            texture.Release();
        }

        texture = new RenderTexture((int)(scale * Screen.width), (int)(scale * Screen.height), 24, RenderTextureFormat.ARGB32);
        image.texture = texture;
        texture.filterMode = filterMode;
        mainCamera.targetTexture = texture;

        currentScale = scale;
        currentFilterMode = filterMode;
    }
}
