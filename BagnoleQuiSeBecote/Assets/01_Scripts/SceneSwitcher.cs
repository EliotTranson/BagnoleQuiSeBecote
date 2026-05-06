using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public int sceneIndex;
    public float switchDelay = 1;
    private int finishedCarCount;
    private bool startSwitch;
    private void OnTriggerEnter(Collider other)
    {
        if (startSwitch) return;
        
        if (other.GetComponent<CarSphereLink>().car.mode.activeMode == CarInputMode.CarMode.Twice)
        {
            StartCoroutine(SwitchScene());
        }
        else
        {
            finishedCarCount++;

            if (finishedCarCount == 2)
            {
                StartCoroutine(SwitchScene());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CarSphereLink>().car.mode.activeMode != CarInputMode.CarMode.Twice)
        {
            finishedCarCount--;
        }
    }

    private IEnumerator SwitchScene()
    {
        startSwitch = true;
        yield return new WaitForSeconds(switchDelay);
        SceneManager.LoadScene(sceneIndex);
    }
}
