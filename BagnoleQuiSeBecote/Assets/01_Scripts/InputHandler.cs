using System;
using CarScripts;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler playerInput1;
    [SerializeField] private PlayerInputHandler playerInput2;
    public CarInput splitCarInput = new CarInput();

    [SerializeField] private TextMeshProUGUI J1Text, J2Text, TwoPlayerText;
    
    private void Update()
    {
        SplitControls();
    }

    private void SplitControls()
    {
        if (playerInput2 == null) return;
        
        splitCarInput.accel = (playerInput1.carInput.accel / 2) + (playerInput2.carInput.accel / 2);
        splitCarInput.decel = (playerInput1.carInput.decel / 2) + (playerInput2.carInput.decel / 2);
        splitCarInput.turn = (playerInput1.carInput.turn / 2) + (playerInput2.carInput.turn / 2);
        
        UpdateDebugUI();
    }

    private void UpdateDebugUI()
    {
        J1Text.text = $"J1 / Accel : {playerInput1.carInput.accel} / Decel : {playerInput1.carInput.decel} / Turn : {playerInput1.carInput.turn}";
        
        J2Text.text = $"J2 / Accel : {playerInput2.carInput.accel} / Decel : {playerInput2.carInput.decel} / Turn : {playerInput2.carInput.turn}";
        
        TwoPlayerText.text = $"Two Player / Accel : {splitCarInput.accel} / Decel : {splitCarInput.decel} / Turn : {splitCarInput.turn}";
    }

    public void InitializePlayerInput(PlayerInput input)
    {
        if (playerInput1 == null)
        {
            playerInput1 = input.gameObject.GetComponent<PlayerInputHandler>();
        }
        else
        {
            playerInput2 = input.gameObject.GetComponent<PlayerInputHandler>();
        }
        
    }
}
