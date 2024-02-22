using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class WASDController : MonoBehaviour
{
    
    public float velocity = 5;
    private Rigidbody rb;
    public float maxVelocity = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //WASD controller
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(velocity*Vector3.forward);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(velocity*Vector3.back);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(velocity*Vector3.left);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(velocity*Vector3.right);
        }
        
        //use normalization and magnitude to maintain maxVelocity or less
        if (rb.velocity.magnitude > maxVelocity)
        {
            Vector3 nor = rb.velocity.normalized*maxVelocity;
            rb.velocity = nor;
        }
    }
}
