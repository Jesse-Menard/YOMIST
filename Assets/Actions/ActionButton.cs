using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    [SerializeField]
    public ActionBaseClass action;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ActionSelected);
    }

    public void ActionSelected()
    {
        if (action == null)
        {
            Debug.LogError("ActionButton: " + name + " has a null action!!");
            return;
        }

        ActionManager.Instance.actionToExecute = action;
    }
}
