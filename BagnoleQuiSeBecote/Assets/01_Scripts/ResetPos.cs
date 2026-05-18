using UnityEngine;

public class ResetPos : MonoBehaviour
{
    public GameObject pos;
    //public SimpleCarController simpleCarController;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            other.transform.position = pos.transform.position;
            other.transform.rotation = pos.transform.rotation;
            other.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            Debug.Log("Touché");
        }
    }
}
