using NUnit.Framework.Internal;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class RealtimeCharacterMover : MonoBehaviour
{
    [SerializeField]
    float acceleration = 8.0f;
    [SerializeField]
    float maxSpeed = 6.0f;

    [SerializeField]
    float jumpHeight = 7.0f;

    Rigidbody2D rb;

    [SerializeField]
    BoxCollider2D groundCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void MoveTo(float xDirection)
    {
        if (xDirection >= 0 ? rb.linearVelocityX < maxSpeed : rb.linearVelocityX > -maxSpeed)
        {
            rb.AddForceX(xDirection * acceleration);

            Mathf.Clamp(rb.linearVelocityX, -maxSpeed, maxSpeed);
        }

        UpdateIsFacingRight(xDirection);
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode2D.Impulse);
        }
    }

    bool IsGrounded()
    {
        Vector3 position = groundCollider.transform.position;
        Collider2D isGroundedCollider = Physics2D.OverlapBox(position, groundCollider.size, 0, LayerMask.GetMask("Ground"));

        return isGroundedCollider;
    }

    void UpdateIsFacingRight(float xValue)
    {
        if (xValue > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (xValue < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}