using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionManager : MonoBehaviour
{
    public static ActionManager Instance;

    public static event EventHandler playerActionExecuted;
    public ActionBaseClass actionToExecute;

    public bool flipToLeft;

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

    private void OnDrawGizmos()
    {
        if (FrameManager.Instance != null && FrameManager.Instance.IsPaused)
        {
            if (actionToExecute is AttackActionBase)
            {

                (Vector3, Vector3) gizmoData = (actionToExecute as AttackActionBase).GetGizmoData();
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(gizmoData.Item1 + PlayerManager.Instance.activePlayerStats.gameObject.transform.position, gizmoData.Item2);
            }
        }
    }

    public void InvokeAction()
    {
        actionToExecute.InvokeAction(PlayerManager.Instance.activePlayerStats);
        
        playerActionExecuted.Invoke(this, EventArgs.Empty);

        FrameManager.Instance.AddFrames(PlayerManager.Instance.activePlayerStats.GetTotalFrames());
    }
}