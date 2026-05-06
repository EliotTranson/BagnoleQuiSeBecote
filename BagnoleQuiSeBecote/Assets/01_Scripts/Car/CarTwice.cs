using System;
using UnityEngine;

public class CarTwice : MonoBehaviour
{
    private CarInputMode mode;
    private SimpleCarController carController;
    private float inputsOffset;
    private float currentSplitTimer;
    private bool hasSplit;
    [SerializeField] private float splitTimer = 1f;
    
    [Header("Prefabs")]
    [SerializeField] private GameObject J1Car;
    [SerializeField] private GameObject J2Car;
    [SerializeField] private Material J1Mat;
    [SerializeField] private Material J2Mat;

    private void Start()
    {
        mode = GetComponent<CarInputMode>();
        carController = GetComponent<SimpleCarController>();
    }
    
    private void Update()
    {
        inputsOffset = InputHandler.Instance.J1Input.turn * InputHandler.Instance.J2Input.turn;
        
        if (inputsOffset < -0.8f)
        {
            if (currentSplitTimer < splitTimer)
            {
                currentSplitTimer += Time.deltaTime;
            }
            else
            {
                if (!hasSplit)
                {
                    CallSplit();
                    hasSplit = true;
                }
            }
        }
        else
        {
            currentSplitTimer = 0;
        }
    }

    private void CallSplit()
    {
        //Instantiate Cars
        GameObject firstCar = Instantiate(J1Car, transform.position, transform.rotation);
        GameObject secondCar = Instantiate(J2Car, transform.position, transform.rotation);
        
        Split(firstCar, CarInputMode.CarMode.J1);
        Split(secondCar, CarInputMode.CarMode.J2);
        
        //Destroy Big Car
        carController.DestroyThisCar();
    }

    private void Split(GameObject car, CarInputMode.CarMode mode)
    {
        //Set Inputs
        car.GetComponent<CarInputMode>().activeMode = mode;

        //Set velocity
        car.GetComponent<SimpleCarController>().rb.linearVelocity = carController.rb.linearVelocity;
        car.GetComponent<SimpleCarController>().rb.angularVelocity = carController.rb.angularVelocity;
        car.GetComponent<SimpleCarController>().speed = carController.speed;
        
        //Set colors
        if (mode == CarInputMode.CarMode.J1)
        {
            car.GetComponent<SimpleCarController>().baseMesh.sharedMaterials[0] = J1Mat;
            car.GetComponent<SimpleCarController>().tuningMesh.sharedMaterial = J1Mat;
        }
        if (mode == CarInputMode.CarMode.J2)
        {
            car.GetComponent<SimpleCarController>().baseMesh.sharedMaterials[0] = J2Mat;
            car.GetComponent<SimpleCarController>().tuningMesh.sharedMaterial = J2Mat;
        }
        
    }
}
