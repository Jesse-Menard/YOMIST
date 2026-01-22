using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    ActionBaseClass QueuedAction;
    GameObject SourceGameObject;
    GameObject GhostGameObject;
    float GhostLifetime = 2.0f;
    Color GhostColor = new Color(0.0f, 1.0f, 0.0f, 0.0f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnGhost()
    {
        GhostGameObject = Instantiate(SourceGameObject);
        GhostGameObject.SetActive(true);
        GhostGameObject.GetComponent<SpriteRenderer>().color = GhostColor;
        QueuedAction.InvokeAction(GhostGameObject.GetComponent<CharacterStats>());
    }
}
