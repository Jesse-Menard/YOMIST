using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    ActionBaseClass action;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(InvokeAction);
    }

    void InvokeAction()
    {
        /// TODO:
        // action.InvokeAction();
    }
}
