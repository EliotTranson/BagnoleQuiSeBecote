using System;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public ParticleSystem particle;
    public MeshRenderer mesh1;
    public MeshRenderer mesh2;
    public MeshRenderer mesh3;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"));
        {
            particle.Play();
            mesh1.enabled = false;
            mesh2.enabled = false;
            mesh3.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
