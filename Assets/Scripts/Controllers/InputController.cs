using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

[RequireComponent(typeof(PlayerInput))]
public class InputController : Controller<InputController>
{
    private PlayerInput pInput;

    public event Action<bool> button0;
    public event Action<bool> button1;
    public event Action<bool> button2;
    public event Action<bool> button3;
    public event Action<bool> button4;
    public event Action<bool> button5;
    public event Action<Vector2> directionalControls;
    public event Action<Vector2> directionalControlsDown;
    public event Action<bool> buttonAny;

    public Vector2 prevDirection;
    public Vector2 prevDirectionDown;
    public bool[] buttonsPressed = new bool[6];

    public bool DebugLog;

    protected override void MyAwake()
    {
        DontDestroyOnLoad(this);
        pInput = GetComponent<PlayerInput>();
        pInput.actions["Move"].performed += MovePerformed;
        pInput.actions["Move"].performed += MovePerformedDown;
        pInput.actions["Move"].canceled += MovePerformed;
        pInput.actions["Button0"].started += (ctx) => ButtonPressed(ctx, 0);
        pInput.actions["Button0"].canceled += (ctx) => ButtonPressed(ctx, 0);
        pInput.actions["Button1"].started += (ctx) => ButtonPressed(ctx, 1);
        pInput.actions["Button1"].canceled += (ctx) => ButtonPressed(ctx, 1);
        pInput.actions["Button2"].started += (ctx) => ButtonPressed(ctx, 2);
        pInput.actions["Button2"].canceled += (ctx) => ButtonPressed(ctx, 2);
        pInput.actions["Button3"].started += (ctx) => ButtonPressed(ctx, 3);
        pInput.actions["Button3"].canceled += (ctx) => ButtonPressed(ctx, 3);
        pInput.actions["Button4"].started += (ctx) => ButtonPressed(ctx, 4);
        pInput.actions["Button4"].canceled += (ctx) => ButtonPressed(ctx, 4);
        pInput.actions["Button5"].started += (ctx) => ButtonPressed(ctx, 5);
        pInput.actions["Button5"].canceled += (ctx) => ButtonPressed(ctx, 5);
    }

    private void MovePerformed(InputAction.CallbackContext ctx)
    {
        Vector2 dir = ctx.ReadValue<Vector2>();
        prevDirection = dir;
        if (DebugLog) Debug.Log("MovePerformed Dir: " + dir);
        directionalControls?.Invoke(prevDirection);
    }
    private void MovePerformedDown(InputAction.CallbackContext ctx)
    {
        Vector2 dir = ctx.ReadValue<Vector2>();
        prevDirectionDown = dir;
        if (DebugLog) Debug.Log("MovePerformed Dir: " + dir);
        directionalControlsDown?.Invoke(prevDirectionDown);
    }
    public void ButtonPressed(InputAction.CallbackContext ctx, int buttonIndex) 
    {
        if (buttonIndex > 5 || buttonIndex < 0) 
        {
            Debug.LogError("Passed in wrong buttonIndex " + buttonIndex);
            return;
        }
        bool val = ctx.ReadValue<float>() > 0.5f;
        buttonsPressed[buttonIndex] = val;
        if (DebugLog) Debug.Log("Button pressed: " + val + " button Index: " + buttonIndex);

        buttonAny?.Invoke(val);
        switch (buttonIndex)
        {
            case 0:
                button0?.Invoke(val);
                return;
            case 1:
                button1?.Invoke(val);
                return;
            case 2:
                button2?.Invoke(val);
                return;
            case 3:
                button3?.Invoke(val);
                return;
            case 4:
                button4?.Invoke(val);
                return;
            case 5:
                button5?.Invoke(val);
                return;
        }
    }
}
