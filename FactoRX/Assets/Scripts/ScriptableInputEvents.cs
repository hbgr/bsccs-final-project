using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Events/InputEvents")]
public class ScriptableInputEvents : ScriptableObject
{
    public event EventHandler<InputAction.CallbackContext> OnMoveEvent;

    public void OnMove(InputAction.CallbackContext context)
    {
        OnMoveEvent?.Invoke(this, context);
    }

    public event EventHandler<InputAction.CallbackContext> OnAction1Event;

    public void OnAction1(InputAction.CallbackContext context)
    {
        OnAction1Event?.Invoke(this, context);
    }

    public event EventHandler<InputAction.CallbackContext> OnAction2Event;

    public void OnAction2(InputAction.CallbackContext context)
    {
        OnAction2Event?.Invoke(this, context);
    }

    public event EventHandler<InputAction.CallbackContext> OnAction3Event;

    public void OnAction3(InputAction.CallbackContext context)
    {
        OnAction3Event?.Invoke(this, context);
    }
}
