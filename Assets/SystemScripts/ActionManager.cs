using System;
using TMPro;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public static ActionManager Instance;

    public static event EventHandler playerActionExecuted;
    public ActionBaseClass actionToExecute;

    [SerializeField]
    TextMeshProUGUI actionTitleText;
    
    bool flipToLeft;

    // Because Unity
    public bool FlipToLeft
    {
        get
        {
            return GetFLipToLeft();
        }
        set
        {
            SetFlipToLeft(value);
        }
    }
    bool GetFLipToLeft()
    {
        return flipToLeft;
    }
    void SetFlipToLeft(bool newBool)
    {
        flipToLeft = newBool;
    }


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
        UpdateSelectedAction(actionToExecute);
    }
    private void OnDrawGizmos()
    {
        if (FrameManager.Instance != null && FrameManager.Instance.IsPaused)
        {
            if (actionToExecute is AttackActionBase)
            {
                (Vector3, Vector3) gizmoData = (actionToExecute as AttackActionBase).GetGizmoData();
                Gizmos.color = Color.red;
                if (flipToLeft)
                {
                    gizmoData.Item1.x *= -1.0f;
                }

                Gizmos.DrawWireCube(gizmoData.Item1 + PlayerManager.Instance.activePlayerStats.gameObject.transform.position, gizmoData.Item2);
            }
        }
    }

    public void InvokeAction()
    {
        if (!FrameManager.Instance.IsPaused)
            return;

        actionToExecute.InvokeAction(PlayerManager.Instance.activePlayerStats);
        
        playerActionExecuted.Invoke(this, EventArgs.Empty);

        FrameManager.Instance.AddFrames(PlayerManager.Instance.activePlayerStats.GetTotalFrames());
    }

    public void UpdateSelectedAction(ActionBaseClass action)
    {
        actionToExecute = action;
        actionTitleText.text = action.name + " Selected";
    }
}