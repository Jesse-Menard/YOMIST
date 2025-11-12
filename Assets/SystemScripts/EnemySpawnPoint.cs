using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField]
    public Vector3 spawnOffset;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + spawnOffset, 0.1f);
    }
}