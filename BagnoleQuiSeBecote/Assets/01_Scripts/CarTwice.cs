using System;
using UnityEngine;

public class CarTwice : MonoBehaviour
{
    private CarInputMode mode;
    private float inputsOffset;

    private void Start()
    {
        mode = GetComponent<CarInputMode>();
    }

    private void Update()
    {
        inputsOffset = InputHandler.Instance.J1Input.turn + InputHandler.Instance.J2Input.turn;
        Debug.Log($"{inputsOffset}");
    }
}
