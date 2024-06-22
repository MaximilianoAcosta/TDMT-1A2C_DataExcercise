using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] Vector2EventChannel MoveEvent;
    [SerializeField] BoolEventChannel SprintEvent;
    [SerializeField] private string moveActionName = "Move";
    [SerializeField] private string runActionName = "Run";

    private void OnEnable()
    {
        var moveAction = inputActions.FindAction(moveActionName);
        if (moveAction != null)
        {
            moveAction.performed += HandleMoveInput;
            moveAction.canceled += HandleMoveInput;
        }
        var runAction = inputActions.FindAction(runActionName);
        if (runAction != null)
        {
            runAction.started += HandleRunInputStarted;
            runAction.canceled += HandleRunInputCanceled;
        }
    }

    private void HandleRunInputStarted(InputAction.CallbackContext ctx)
    {
        //DONE TODO: Implement event logic
        SprintEvent.OnEventRaised?.Invoke(true);
        Debug.Log($"{name}: Run input started");
    }

    private void HandleRunInputCanceled(InputAction.CallbackContext ctx)
    {
        //DONE TODO: Implement event logic
        SprintEvent.OnEventRaised?.Invoke(false);
        Debug.Log($"{name}: Run input canceled");
    }

    private void HandleMoveInput(InputAction.CallbackContext ctx)
    {
        //DONE TODO: Implement event logic

        MoveEvent.OnEventRaised?.Invoke(ctx.ReadValue<Vector2>());

    }
}
