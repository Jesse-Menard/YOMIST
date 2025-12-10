using System;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField]
    float health = 100;
    float maxHealth;
    int hitStunFrames = 0;
    int endLagFrames = 0;

    [SerializeField]
    AttackActionBase attack;

    private void Awake()
    {
        maxHealth = health;
        FrameManager.FrameTick += TickCountersDownEvent;
    }

    private void OnDestroy()
    {
        FrameManager.FrameTick -= TickCountersDownEvent;
    }

    private void Start()
    {
        if (CompareTag("Player"))
        {
            PlayerManager.Instance.activePlayerStats = this;
        }
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

    public void AddHitStun(int stunFrames)
    {
        if (hitStunFrames < stunFrames)
        {
            hitStunFrames = stunFrames;

            if (CompareTag("Player"))
            {
                FrameManager.Instance.SetFrames(hitStunFrames, true);
            }

            endLagFrames = 0;
        }
    }

    void TickCountersDownEvent(object sender, EventArgs e)
    {
        TickCountersDown();
    }

    void TickCountersDown()
    {
        if (endLagFrames > 0)
        {
            endLagFrames--;
            GetComponent<SpriteRenderer>().color = Color.orange;
        }
        if (hitStunFrames > 0)
        {
            hitStunFrames--;
            GetComponent<SpriteRenderer>().color = new Color(0f, .5f, 1f, 1f);
        }
        
        if (endLagFrames <= 0 && hitStunFrames <= 0)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public int GetTotalFrames()
    {
        return endLagFrames + hitStunFrames;
    }

    public bool IsInHitStun()
    {
        return hitStunFrames > 0;
    }

    public void AddEndLag(int framesToAdd)
    {
        endLagFrames = framesToAdd;
    }

    public bool IsOnCooldown()
    {
        return endLagFrames > 0;
    }

    public void CallOnlyAttack()
    {
        attack.InvokeAction(this);
    }

    private void OnDrawGizmos()
    {
        if (CompareTag("Player"))
            return;

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
        if (CompareTag("Player"))
        {
            /// TODO: Replace with proper GameOver screen
            Application.Quit();
        }
        else
        {
            FindFirstObjectByType<EnemyManager>().AddEnemyToQueue(gameObject);
        }
    }

    public void ResetHealth()
    {
        health = maxHealth;
    }
}