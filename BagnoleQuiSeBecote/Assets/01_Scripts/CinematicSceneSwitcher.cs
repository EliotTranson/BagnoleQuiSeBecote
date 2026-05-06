using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicSceneSwitcher : MonoBehaviour
{
    public float changeTime;
    public int sceneIndex;

    private void Update()
    {
        changeTime -= Time.deltaTime;
        if (changeTime < 0)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
