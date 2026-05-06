using System;
using UnityEngine;

public class DestroyableWall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CarSphereLink>())
        {
            
        }
    }
}
