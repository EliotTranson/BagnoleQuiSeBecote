using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cineCam; 
    private SimpleCarController carController;
    [SerializeField] private ParticleSystem psSpeedLines;
    private float shakeTimer;
    private CinemachineBasicMultiChannelPerlin perlin;
    private float shakeBase, shakeSplit;

    [SerializeField] private float speedLinesThreshold = 20;
    private float carSpeed;
    private bool isMaxSpeed;

    private void Start()
    {
        perlin = cineCam.GetComponent<CinemachineBasicMultiChannelPerlin>();
        carController = GetComponent<SimpleCarController>();
    }
    
    private void Update()
    {
        ShakeUpdate();
        SpeedUpdate();
    }

    private void SpeedUpdate()
    {
        //SPEED LINES
        Debug.Log(carController.speed);
        
        carSpeed = carController.rb.linearVelocity.magnitude;
        if (carController.speed > 0 && carSpeed > speedLinesThreshold && !isMaxSpeed)
        {
            isMaxSpeed = true;
            psSpeedLines.Play();
        }

        if (carSpeed < speedLinesThreshold * 0.75f && isMaxSpeed)
        {
            isMaxSpeed = false;
            psSpeedLines.Stop();
        }
    }
    
    private void ShakeUpdate()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
                //Time Over!
                shakeBase = 0;
            }
        }
        
        perlin.FrequencyGain = 0.5f + shakeBase + shakeSplit;
    }

    public void ShakeCamera(float intensity, float time)
    {
        shakeBase = intensity;
        shakeTimer = time;
    }

    public void SetSplitShake(float intensity)
    {
        shakeSplit = intensity;
    }
}
