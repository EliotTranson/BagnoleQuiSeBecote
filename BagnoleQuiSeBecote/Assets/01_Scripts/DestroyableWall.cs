using System;
using UnityEngine;

public class DestroyableWall : MonoBehaviour
{
    private SimpleCarController carController;
    [SerializeField] private ParticleSystem psExplosion;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CarSphereLink>())
        {
            carController = other.gameObject.GetComponent<CarSphereLink>().car;
            
            if (carController.mode.activeMode == CarInputMode.CarMode.Twice && carController.isDashing)
            {
                DestroyWall();
            }
        }
    }

    private void DestroyWall()
    {
        //Debug.Log("Destroy");
        Instantiate(psExplosion, null);
        Destroy(gameObject);
    }
}
