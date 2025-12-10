using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "EmptyActionBase", menuName = "Action Data/Generic/EmptyAction")]
public class ActionBaseClass : ScriptableObject
{
    [Header("Icon")]
    [SerializeField]
    public Sprite actionIcon;

    [Header("Frame Data")]
    [SerializeField]
    protected int endLagFrames;
    [SerializeField]
    protected int activeFrames;
    [SerializeField]
    protected int startupFrames;

    public virtual void InvokeAction(CharacterStats owner)
    {
        owner.AddEndLag(endLagFrames);
        owner.GetComponent<SpriteRenderer>().flipX = ActionManager.Instance.FlipToLeft;
    }

    public void UpdateButtonImage(Image imageToChange)
    {
        imageToChange.sprite = actionIcon;
    }
}