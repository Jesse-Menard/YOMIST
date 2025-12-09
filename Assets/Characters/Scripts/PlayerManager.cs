using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public CharacterStats activePlayerStats;

    public static PlayerManager Instance;

    private void Awake()
    {
        // If another instance already exists destroy this one
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}