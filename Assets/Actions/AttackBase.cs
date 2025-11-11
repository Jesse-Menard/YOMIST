using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "SimplePlayerAttack", menuName = "Attack Data/SimplePlayerAttack")]
[Serializable]
public class AttackBase : ScriptableObject
{
    [SerializeField]
    float damage;
    [SerializeField]
    float hitStun;
    [SerializeField]
    float cooldown;
    [SerializeField]
    Vector2 knockbackAngle;
    [SerializeField]
    float knockbackForce;
    [SerializeField]
    Vector2 hitBoxSize;
    [SerializeField]
    Vector3 hitBoxOffset;

    public void Attack(GameObject owner)
    {
        Debug.Log("ATTACK!");

        Vector3 position = hitBoxOffset + owner.transform.position;

        Vector2 modifiedKnockbackAngle = knockbackAngle;

        // Flips X. Feels jank but efficient enough for now
        if (owner.GetComponent<SpriteRenderer>().flipX)
        {
            position.x -= hitBoxOffset.x * 2;
            modifiedKnockbackAngle = new Vector2(knockbackAngle.x * -1, knockbackAngle.y);
        }

        bool isPlayer = owner.CompareTag("Player");
        LayerMask attackMask = isPlayer ? LayerMask.GetMask("Player") : LayerMask.GetMask("Player");
        
        Collider2D[] hitTargets = Physics2D.OverlapBoxAll(position, hitBoxSize, 0, attackMask);

        foreach (Collider2D hitTarget in hitTargets)
        {
            if (hitTarget.gameObject)
                Debug.Log(owner.name + " HIT " + hitTarget.gameObject.name);
    
            hitTarget.GetComponent<CharacterStats>().Damage(damage);
            hitTarget.GetComponent<Rigidbody2D>().AddForce(modifiedKnockbackAngle.normalized * knockbackForce, ForceMode2D.Impulse);
            hitTarget.GetComponent<CharacterStats>().AddHitStun(hitStun);
        }

        owner.GetComponent<CharacterStats>().AddCooldown(cooldown);
    }

    public (Vector3 offset, Vector3 size) GetGizmoData()
    {
        Vector3 size = new Vector3(hitBoxSize.x, hitBoxSize.y, 1.0f);
        return (hitBoxOffset, size);
    }
}