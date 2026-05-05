using UnityEngine;

public class ResetPos : MonoBehaviour
{
    public GameObject pos;
    public GameObject player;
    public SimpleCarController simpleCarController;
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
            player.transform.position = pos.transform.position;
            player.transform.rotation = pos.transform.rotation;
            player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            Debug.Log("Touché");
        }
    }
}
