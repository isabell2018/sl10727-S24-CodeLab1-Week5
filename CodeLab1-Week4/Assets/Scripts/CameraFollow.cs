using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;

    private Rigidbody rb;
    private float t = 0.725f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newp = Vector3.Lerp(rb.transform.position, player.transform.position,t);
        rb.transform.position = new Vector3(newp.x,newp.y+8, newp.z-6);
    }
}
