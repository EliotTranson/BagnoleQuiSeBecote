using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public CarInput carInput;

    [SerializeField] private float accel, decel, turn;

    private void Update()
    {
        accel = carInput.accel;
        decel = carInput.decel;
        turn = carInput.turn;
    }

    public void OnAccel(InputAction.CallbackContext context)
    {
        carInput.accel = context.ReadValue<float>();
    }
    
    public void OnDecel(InputAction.CallbackContext context)
    {
        carInput.decel = context.ReadValue<float>();
    }
    
    public void OnTurn(InputAction.CallbackContext context)
    {
        carInput.turn = context.ReadValue<Vector2>().x;
    }
}
