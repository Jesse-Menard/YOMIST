using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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

    void Start()
    {
        AudioManager.Instance.Initialize();
        AudioManager.Instance.PlaySound(Sound.MENU_MUSIC, AudioChannel.MUSIC, true);
    }
}