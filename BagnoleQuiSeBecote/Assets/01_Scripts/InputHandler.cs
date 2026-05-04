using System;
using CarScripts;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler playerInput1;
    [SerializeField] private PlayerInputHandler playerInput2;
    
    public CarInput J1Input, J2Input, TwiceInput;

    [SerializeField] private TextMeshProUGUI J1Text, J2Text, TwoPlayerText;
    
    public static InputHandler Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }
    
    private void Update()
    {
        SetCarInputs();
    }

    private void SetCarInputs()
    {
        if (playerInput2 == null) return;
        
        J1Input.accel = playerInput1.accel;
        J1Input.decel = playerInput1.decel;
        J1Input.turn = playerInput1.turn;
        
        J2Input.accel = playerInput2.accel;
        J2Input.decel = playerInput2.decel;
        J2Input.turn = playerInput2.turn;
        
        TwiceInput.accel = (J1Input.accel / 2) + (J2Input.accel / 2);
        TwiceInput.decel = (J1Input.decel / 2) + (J2Input.decel / 2);
        TwiceInput.turn = (J1Input.turn / 2) + (J2Input.turn / 2);
        
        UpdateDebugUI();
    }

    private void UpdateDebugUI()
    {
        J1Text.text = $"J1 / Accel : {J1Input.accel} / Decel : {J1Input.decel} / Turn : {J1Input.turn}";
        
        J2Text.text = $"J2 / Accel : {J2Input.accel} / Decel : {J2Input.decel} / Turn : {J2Input.turn}";
        
        TwoPlayerText.text = $"Two Player / Accel : {TwiceInput.accel} / Decel : {TwiceInput.decel} / Turn : {TwiceInput.turn}";
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
