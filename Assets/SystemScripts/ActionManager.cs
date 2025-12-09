using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionManager : MonoBehaviour
{
    public static ActionManager Instance;

    public static event EventHandler playerActionExecuted;
    public ActionBaseClass actionToExecute;

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

    public void InvokeAction()
    {
        actionToExecute.InvokeAction(PlayerManager.Instance.activePlayerStats);
        
        playerActionExecuted.Invoke(this, EventArgs.Empty);

        FrameManager.Instance.AddFrames(PlayerManager.Instance.activePlayerStats.GetTotalFrames());
    }
}