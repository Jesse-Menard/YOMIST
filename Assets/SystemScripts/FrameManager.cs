using System;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class FrameManager : MonoBehaviour
{    
    public static event EventHandler FrameTick;
    [SerializeField]
    private int framesToSkip = 500;
    public bool IsPaused
    {
        get
        {
            return framesToSkip <= 0;
        }
    }

    public static FrameManager Instance;

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

    private void Start()
    {
        ActionManager.playerActionExecuted += OnPlayerActionConfirm;
    }

    private void FixedUpdate()
    {
        if (framesToSkip > 0)
        {
            framesToSkip--;
            FrameTick.Invoke(this, EventArgs.Empty);
            Debug.Log("Frame Ticked, " + framesToSkip + " frames to go");
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

    private void OnPlayerActionConfirm(object sender, EventArgs e)
    {
        ResetTimeScale();
    }
}