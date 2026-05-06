using System;
using UnityEngine;

public class CarInputMode : MonoBehaviour
{
    public enum CarMode
    {
        None, J1, J2, Twice
    }
    public CarMode activeMode;

    private SimpleCarController car;

    private void Start()
    {
        car = GetComponent<SimpleCarController>();
    }

    private void Update()
    {
        switch (activeMode)
        {
            case CarMode.None : 
                car.input = new CarInput();
                break;
            case CarMode.J1 : 
                car.input = InputHandler.Instance.J1Input;
                break;
            case CarMode.J2 : 
                car.input = InputHandler.Instance.J2Input;
                break;
            case CarMode.Twice : 
                car.input = InputHandler.Instance.TwiceInput;
                break;
        }
    }
}
