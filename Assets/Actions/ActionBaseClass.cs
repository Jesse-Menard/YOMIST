using UnityEngine;

public abstract class ActionBaseClass : ScriptableObject
{
    [SerializeField]
    protected int endLagFrames;
    [SerializeField]
    protected int activeFrames;
    [SerializeField]
    protected int startupFrames;

    public abstract void InvokeAction(CharacterStats owner);
}