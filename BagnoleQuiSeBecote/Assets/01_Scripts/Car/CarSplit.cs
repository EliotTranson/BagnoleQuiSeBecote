using System;
using System.Collections;
using UnityEngine;

public class CarSplit : MonoBehaviour
{
    private SimpleCarController carController;
    private bool canMerge;
    private SimpleCarController otherCar;

    [SerializeField] private GameObject bigCar;

    private void Start()
    {
        carController = GetComponent<SimpleCarController>();
        StartCoroutine(EnableMerge());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && canMerge)
        {
            otherCar = other.GetComponent<CarSphereLink>().J2Car;
            CallMerge();
        }
    }

    private IEnumerator EnableMerge()
    {
        yield return new WaitForSeconds(1f);
        canMerge = true;
    }
    
    private void CallMerge()
    {
        //Instantiate Cars
        GameObject car = Instantiate(bigCar, transform.position, transform.rotation);
        
        Merge(car);
        
        //Destroy Big Car
        Destroy(otherCar.gameObject);
        Destroy(gameObject);
    }

    private void Merge(GameObject car)
    {
        //Set velocity

        Vector3 newLinearVelocity = carController.rb.linearVelocity + otherCar.rb.linearVelocity;
        Vector3 newAngularVelocity = carController.rb.angularVelocity + otherCar.rb.angularVelocity;
        
        car.GetComponent<SimpleCarController>().rb.linearVelocity = newLinearVelocity;
        car.GetComponent<SimpleCarController>().rb.angularVelocity = newAngularVelocity;
        car.GetComponent<SimpleCarController>().speed = (carController.speed + otherCar.speed)/2;
    }
}
