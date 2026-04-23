using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public CharacterStats activePlayerStats = null;

    [SerializeField]
    public Slider HealthBar;
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

    public void Update()
    {
        UpdateHealthBar();
    }

    /// TODO:
    /// Only update when player health changes (ex. player sends an event whe nmodified)
    void UpdateHealthBar()
    {
        if (activePlayerStats != null)
        {
            HealthBar.value = activePlayerStats.GetHealth() / activePlayerStats.GetMaxHealth();
        }
    }
}