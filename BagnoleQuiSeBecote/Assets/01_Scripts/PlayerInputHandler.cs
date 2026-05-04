using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public float accel, decel, turn, jump;

    public void OnAccel(InputAction.CallbackContext context)
    {
        accel = context.ReadValue<float>();
    }
    
    public void OnDecel(InputAction.CallbackContext context)
    {
        decel = context.ReadValue<float>();
    }
    
    public void OnTurn(InputAction.CallbackContext context)
    {
        turn = context.ReadValue<Vector2>().x;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.action.WasPressedThisFrame()) jump = 0.5f;
        if(context.action.WasReleasedThisFrame()) jump = 0;
    }
}
