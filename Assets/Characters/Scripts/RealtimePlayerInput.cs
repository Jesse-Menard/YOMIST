using UnityEngine;
using UnityEngine.InputSystem;

public class RealtimePlayerInput : MonoBehaviour
{
    [SerializeField]
    InputActionAsset inputActions;

    InputAction moveInput;
    InputAction jumpInput;
    InputAction attackInput;

    void Awake()
    {
        moveInput = inputActions.FindAction("Move");

        jumpInput = inputActions.FindAction("Jump");
        jumpInput.performed += CallJump;

        attackInput = inputActions.FindAction("Attack");
        attackInput.performed += CallAttack;
    }

    void Update()
    {
        if (InHitStun())
            return;

        gameObject.GetComponent<RealtimeCharacterMover>().MoveTo(moveInput.ReadValue<float>());
    }

    void CallJump(InputAction.CallbackContext context)
    {
        if (InHitStun())
            return;

        GetComponent<RealtimeCharacterMover>().Jump();
    }

    void CallAttack(InputAction.CallbackContext context)
    {
        if (InHitStun())
            return;
        if (GetComponent<CharacterStats>().IsOnCooldown())
            return;

        GetComponent<CharacterStats>().CallOnlyAttack();
    }

    bool InHitStun()
    {
        return GetComponent<CharacterStats>().IsInHitStun();
    }
}