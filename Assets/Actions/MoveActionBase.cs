using UnityEngine;

public class MoveActionBase : ActionBaseClass
{
    [SerializeField]
    float speedMultiplier;
    [SerializeField]
    Vector2 movementDirection;

    /// TODO: MoveAction
    public override void InvokeAction(CharacterStats owner)
    {
        throw new System.NotImplementedException();
    }
}
