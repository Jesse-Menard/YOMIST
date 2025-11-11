using UnityEngine;
using UnityEngine.UIElements;

public class CharacterStats : MonoBehaviour
{
    [SerializeField]
    float health = 100;
    float maxHealth;
    float stunDuration = 0;
    float cooldown = 0;

    [SerializeField]
    AttackBase attack;

    private void FixedUpdate()
    {
        TickCountersDown();
    }

    public void Damage(float damage)
    {
        if (damage <= 0)
        {
            Debug.Log("Damage passed should be positive");
            return;
        }

        health -= damage;

        if (health <= 0)
        {
            Death();
        }
    }

    public void AddHitStun(float stunSeconds)
    {
        if (stunDuration < stunSeconds)
            stunDuration = stunSeconds;
    }

    void TickCountersDown()
    {
        if (cooldown > 0)
        {
            cooldown-= Time.deltaTime;
            GetComponent<SpriteRenderer>().color = Color.orange;
        }
        if (stunDuration > 0)
        {
            stunDuration -= Time.deltaTime;
            GetComponent<SpriteRenderer>().color = new Color(.5f, .5f, 1f, 1f);
        }
        
        if (cooldown <= 0 && stunDuration <= 0)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public bool IsInHitStun()
    {
        return stunDuration > 0;
    }

    public void AddCooldown(float cooldownSeconds)
    {
        cooldown = cooldownSeconds;
    }

    public bool IsOnCooldown()
    {
        return cooldown > 0;
    }

    public void CallOnlyAttack()
    {
        attack.Attack(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 modifiedOffset = attack.GetGizmoData().offset;

        if (GetComponent<SpriteRenderer>().flipX)
        {
            modifiedOffset.x *= -1;
        }

        Gizmos.DrawWireCube(transform.position + modifiedOffset , attack.GetGizmoData().size);
    }

    void Death()
    {
        Destroy(gameObject);
    }
}