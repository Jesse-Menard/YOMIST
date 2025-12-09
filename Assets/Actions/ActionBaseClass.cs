using UnityEngine;

public abstract class ActionBaseClass : ScriptableObject
{
    [SerializeField]
    protected int endLagFrames;

    public abstract void InvokeAction();
}