using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleAttackBase", menuName = "Action Data/Attacks/SimpleAttackBase")]
[Serializable]
public class AttackActionBase : MoveActionBase
{
    [Header("Attack Data")]
    [SerializeField]
    float damage;
    [SerializeField]
    int hitStunFrames;
    [SerializeField]
    Vector2 knockbackAngle;
    [SerializeField]
    float knockbackForce;

    [Header("Hitbox")]
    [SerializeField]
    Vector2 hitBoxSize;
    [SerializeField]
    Vector3 hitBoxOffset;

    public override void InvokeAction(CharacterStats owner)
    {
        base.InvokeAction(owner);
        Debug.Log("ATTACK!");

        Vector3 position = hitBoxOffset + owner.transform.position;

        Vector2 modifiedKnockbackAngle = knockbackAngle;

        // Flips X. Feels jank but efficient enough for now
        if (ActionManager.Instance.flipToLeft)
        {
            position.x -= hitBoxOffset.x * 2;
            modifiedKnockbackAngle = new Vector2(knockbackAngle.x * -1, knockbackAngle.y);
        }

        bool isPlayer = owner.CompareTag("Player");
        LayerMask attackMask = isPlayer ? LayerMask.GetMask("Enemy") : LayerMask.GetMask("Player");
        
        Collider2D[] hitTargets = Physics2D.OverlapBoxAll(position, hitBoxSize, 0, attackMask);

        foreach (Collider2D hitTarget in hitTargets)
        {
            if (hitTarget.gameObject)
                Debug.Log(owner.name + " HIT " + hitTarget.gameObject.name);
    
            hitTarget.GetComponent<CharacterStats>().Damage(damage);
            hitTarget.GetComponent<Rigidbody2D>().AddForce(modifiedKnockbackAngle.normalized * knockbackForce, ForceMode2D.Impulse);
            hitTarget.GetComponent<CharacterStats>().AddHitStun(hitStunFrames);
        }

        owner.GetComponent<CharacterStats>().AddEndLag(endLagFrames);
    }

    public (Vector3 offset, Vector3 size) GetGizmoData()
    {
        Vector3 size = new Vector3(hitBoxSize.x, hitBoxSize.y, 1.0f);
        return (hitBoxOffset, size);
    }
}