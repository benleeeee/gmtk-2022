using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    static CharacterController cc;
    static Rigidbody rb;

    public int movementSpeed = 10;

 
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movementDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (movementDirection.magnitude >= 0.1f)
        {
            cc.Move(movementDirection * movementSpeed * Time.deltaTime);
        }
    }

    
}
