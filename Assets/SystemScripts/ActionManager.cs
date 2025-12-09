using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public static ActionManager Instance;

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