using System;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static event EventHandler FrameTick;

    int framesToSkip = 0;
    private void Awake()
    {
        // If another instance already exists destroy this one
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void FixedUpdate()
    {
        if (framesToSkip > 0)
        {
            framesToSkip--;
            FrameTick.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 0;
        } 
    }

    public void SetFrames(int frames, bool shouldOverride = false)
    {
        if (shouldOverride || frames > framesToSkip)
        {
            framesToSkip = frames;
            ResetTimeScale();
        }
    }

    public void AddFrames(int frames)
    {
        framesToSkip += frames;
        ResetTimeScale();
    }

    public void ResetTimeScale()
    {
        Time.timeScale = 1.0f;
    }
}