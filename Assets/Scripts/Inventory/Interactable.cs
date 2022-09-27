using Entities.Player;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //[Inject] protected Player Player { get; private set; }
    enum InputType { KeyDown, KeyPressed, KeyUp }
    [SerializeField] protected KeyCode inputKey = KeyCode.F;
    [SerializeField] private InputType inputType = InputType.KeyDown;
    [SerializeField] private bool showDescription = true;
    [SerializeField] private float radius = 3.0f;
    public Transform interactionTransform;

    private bool _isDescriptionViewed;

    private void Start()
    {
        if (interactionTransform == null)
            interactionTransform = transform;
    }

    private void Update()
    {
        bool isInArea = Vector3.Distance(Player.Instance.transform.position, interactionTransform.position) <= radius;
        if (isInArea && showDescription && !_isDescriptionViewed) ShowDescription();
        if (!isInArea && showDescription && _isDescriptionViewed) ClearDescription();
        if (ReadInput() && isInArea) Interact();
    }

    protected virtual void ShowDescription() => _isDescriptionViewed = true;
    protected virtual void ClearDescription() => _isDescriptionViewed = false;
    protected virtual void Interact() {}
    
    private bool ReadInput()
    {
        switch (inputType)
        {
            case InputType.KeyDown: return Input.GetKeyDown(inputKey);
            case InputType.KeyPressed: return Input.GetKey(inputKey);
            case InputType.KeyUp: return Input.GetKeyUp(inputKey);
            default: return false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null) 
            interactionTransform = transform;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
