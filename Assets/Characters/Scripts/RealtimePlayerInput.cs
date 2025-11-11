using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RealtimePlayerInput : MonoBehaviour
{
    [SerializeField]
    InputActionAsset inputActions;

    InputAction moveInput;
    InputAction jumpInput;

    void Start()
    {
        moveInput = inputActions.FindAction("Move");
        jumpInput = inputActions.FindAction("Jump");
        jumpInput.performed += CallJump;
    }

    void Update()
    {
        gameObject.GetComponent<RealtimeCharacterMover>().MoveTo(moveInput.ReadValue<float>());
    }

    void CallJump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump");
        GetComponent<RealtimeCharacterMover>().Jump();
    }
}