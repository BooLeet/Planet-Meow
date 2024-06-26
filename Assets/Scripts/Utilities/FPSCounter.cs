﻿using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    public static FPSCounter instance;

    float deltaTime = 0.0f;
    public float height = 100;
    public bool show = false;
    public Font font;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    public void Show()
    {
        show = true;
    }

    public void Hide()
    {
        show = false;
    }

    void OnGUI()
    {
        if (!show)
            return;

        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(10, 0, w, h * 4 / height);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = (int)(h * 2 / height);
        if (font)
        {
            style.font = font;
        }
        style.normal.textColor = Color.white;
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
}
