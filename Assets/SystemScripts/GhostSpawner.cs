using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;

public class GhostSpawner : MonoBehaviour
{
    public GameObject GhostGameObject;
    static int GhostActionLifetime = 10;
    static int GhostHoldTime = 40;
    static int GhostTimer = 1;
    static bool bHideGhosts = false;

    Color GhostColor = new Color(0.0f, 1.0f, 0.0f, 0.4f);

    void FixedUpdate()
    {
        if (GhostGameObject && FrameManager.Instance.IsPaused)
        {
            if (CompareTag("Player"))
                GhostTimer++;

            if (GhostTimer == GhostActionLifetime)
                GhostGameObject.GetComponent<RealtimeCharacterMover>().PauseMomentum();

            if (GhostTimer > GhostActionLifetime + GhostHoldTime)
            {
                foreach (GameObject enemy in FindAnyObjectByType<EnemyManager>().activeEnemies)
                {
                    enemy.GetComponent<GhostSpawner>().ResetGhost();
                }

                if (CompareTag("Player"))   
                    ResetGhost();
            }
        }
    }

    public void HideGhosts()
    {
        bHideGhosts = true;

        if (GhostGameObject)
            GhostGameObject.SetActive(!bHideGhosts);
    }
    
    public void ShowGhosts()
    {
        bHideGhosts = false;

        if (GhostGameObject)
            GhostGameObject.SetActive(!bHideGhosts);
    }

    public void SpawnGhost()
    {
        // If Ghost already made, or if this instance is a ghost
        if (CompareTag("Ghost"))
            return;

        if (GhostGameObject != null)
        {
            ResetGhost();
            return;
        }

        GhostGameObject = Instantiate(gameObject, transform.parent);
        GhostGameObject.GetComponent<SpriteRenderer>().color = GhostColor;
        GhostGameObject.SetActive(!bHideGhosts);

        if (CompareTag("Player"))
        {
            GhostGameObject.layer = LayerMask.NameToLayer("PlayerGhost");
        }
        else if (CompareTag("Enemy"))
        {
            GhostGameObject.GetComponent<GenericEnemyAI>().player =
                GhostGameObject.GetComponent<GenericEnemyAI>().player.GetComponent<GhostSpawner>().GhostGameObject;

            GhostGameObject.layer = LayerMask.NameToLayer("EnemyGhost");
        }

        GhostGameObject.tag = "Ghost";
        ResetGhost();
    }

    public void ResetGhost()
    {
        if (GhostGameObject == null)
        {
            SpawnGhost();
            return;
        }

        ShowGhosts();

        GhostTimer = 0;
        GhostGameObject.transform.position = transform.position;
        GhostGameObject.GetComponent<CharacterStats>().SetHealth(GetComponent<CharacterStats>().GetHealth());

        GhostGameObject.GetComponent<RealtimeCharacterMover>().PauseMomentum();
        GhostGameObject.GetComponent<RealtimeCharacterMover>().StoredMomentum = GetComponent<RealtimeCharacterMover>().StoredMomentum;
        GhostGameObject.GetComponent<RealtimeCharacterMover>().ResumeMomentum();

        if (GhostGameObject.layer == LayerMask.NameToLayer("PlayerGhost"))
        {
            ActionManager.Instance.actionToExecute.InvokeAction(GhostGameObject.GetComponent<CharacterStats>(), true);
            GhostActionLifetime = ActionManager.Instance.actionToExecute.actionTotalFrames;
        }
    }
}
