using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CallMrWheel : MonoBehaviour
{
    public Image mrPneu;
    public Image backGround;
    public TextMeshProUGUI text;

    public float timer = 3f;
    public float duration;
    
    void Start()
    {
        mrPneu.enabled = false;
        text.enabled = false;
        backGround.enabled = false;
    }
    void Update()
    {
        if (duration > 0)
        {
            duration -= Time.deltaTime;
            mrPneu.enabled = true;
            text.enabled = true;
            backGround.enabled = true;
        }
        else
        {
            mrPneu.enabled = false;
            text.enabled = false;
            backGround.enabled = false;
        }
    }

    public void Call()
    {
        duration = timer;
    }
}
