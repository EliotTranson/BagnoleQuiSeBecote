using System;
using Unity.Cinemachine;
using UnityEngine;

public class CinemachineShake : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cineCam;
    private float shakeTimer;
    private CinemachineBasicMultiChannelPerlin perlin;
    private float shakeBase, shakeSplit;

    private void Start()
    {
        perlin = cineCam.GetComponent<CinemachineBasicMultiChannelPerlin>();
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

    private void Update()
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
}
