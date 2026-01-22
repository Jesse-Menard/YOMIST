using UnityEngine;

public class GenericEnemyAI : MonoBehaviour
{
    GameObject player;
    float jumpCooldown = 0;
    public float HorizontalAttackDistance = 1.8f;
    public float VerticalAttackDistance = 1.0f;

    private void Awake()
    {
        player = FindFirstObjectByType<RealtimePlayerInput>().gameObject;
    }

    void Update()
    {
        if (GetComponent<CharacterStats>().IsInHitStun() || FrameManager.Instance.IsPaused)
            return;

        Move();
        Jump();
        Attack();
    }

    void Move()
    {
        if (player.transform.position.x > transform.position.x)
        {
            GetComponent<RealtimeCharacterMover>().MoveTo(1);
        }
        else
        {
            GetComponent<RealtimeCharacterMover>().MoveTo(-1);
        }
    }

    void Jump()
    {
        if (jumpCooldown > 0) 
        {
            jumpCooldown -= Time.deltaTime; 
            return;
        }
        
        if (player.transform.position.y > transform.position.y + 1.0f)
        {
            GetComponent<RealtimeCharacterMover>().Jump();
            jumpCooldown = 0.2f;
        }
    }

    void Attack()
    {
        if (GetComponent<CharacterStats>().IsOnCooldown())
            return;

        Vector3 deltaPos = player.transform.position - transform.position;
        float distance = deltaPos.magnitude;
        if (Mathf.Abs(deltaPos.x) < HorizontalAttackDistance && Mathf.Abs(deltaPos.y) < VerticalAttackDistance)
        {
            GetComponent<CharacterStats>().CallOnlyAttack();
        }
    }
}