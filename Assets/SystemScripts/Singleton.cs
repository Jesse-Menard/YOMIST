using UnityEngine;

/// TODO: Singleton base WIP
abstract public class Singleton<T> : MonoBehaviour
{
    static Singleton<T> instance;
    T data;

    static public T Instance
    {
        get
        {
            return instance.data;
        }
    }

    private void Awake()
    {
        // If another instance already exists destroy this one
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }
}