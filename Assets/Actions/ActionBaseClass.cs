using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "EmptyActionBase", menuName = "Action Data/Generic/EmptyAction")]
public class ActionBaseClass : ScriptableObject
{
    [Header("Icon")]
    [SerializeField]
    public Sprite actionIcon;

    public int actionTotalFrames
    {
        get { return endLagFrames; }// + activeFrames + startupFrames; }
    }

    [Header("Frame Data")]
    [SerializeField]
    protected int endLagFrames;
    [SerializeField]
    protected int activeFrames;
    [SerializeField]
    protected int startupFrames;

    public virtual void InvokeAction(CharacterStats owner, bool keepTimePaused = false)
    {
        if (!keepTimePaused)
            owner.AddEndLag(endLagFrames);
    
        owner.GetComponent<SpriteRenderer>().flipX = ActionManager.Instance.FlipToLeft;
    }

    public void UpdateButtonImage(Image imageToChange)
    {
        imageToChange.sprite = actionIcon;
    }
}