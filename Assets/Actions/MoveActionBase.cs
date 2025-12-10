using UnityEngine;

[CreateAssetMenu(fileName = "MoveAction", menuName = "Action Data/Movement/MoveAction")]
public class MoveActionBase : ActionBaseClass
{
    [Header("Movement Data")]
    [SerializeField]
    float speedMultiplier;
    [SerializeField]
    Vector2 movementDirection;

    [SerializeField]
    bool isImpulse = false;

    public override void InvokeAction(CharacterStats owner)
    {
        base.InvokeAction(owner);
        if (speedMultiplier == 0)
            return;

        Rigidbody2D ownerRB = owner.GetComponent<Rigidbody2D>();
        ForceMode2D forceMode = isImpulse ? ForceMode2D.Impulse : ForceMode2D.Force;

        Vector2 newMoveDir = movementDirection.normalized;

        if (ActionManager.Instance.FlipToLeft)
            newMoveDir.x *= -1.0f;

        ownerRB.AddForce(newMoveDir * speedMultiplier, forceMode);
    }
}