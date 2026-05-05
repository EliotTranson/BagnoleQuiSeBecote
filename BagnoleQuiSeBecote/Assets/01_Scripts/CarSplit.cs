using System;
using UnityEngine;

public class CarSplit : MonoBehaviour
{
    private SimpleCarController carController;

    private void Start()
    {
        carController = GetComponent<SimpleCarController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Assembly");
        }
    }
}
