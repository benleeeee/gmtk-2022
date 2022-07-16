using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dice : MonoBehaviour
{
    static Rigidbody rb;
    public static Vector3 diceVelocity;

    private bool RollDice = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        diceVelocity = rb.velocity;

        if (RollDice)
        {
            float dirX = Random.Range(0, 500);
            float dirY = Random.Range(0, 500);
            float dirZ = Random.Range(0, 500);

            transform.position = new Vector3(0, 2, 0);
            transform.rotation = Quaternion.identity;

            rb.AddForce(transform.up * 500);
            rb.AddTorque(dirX, dirY, dirZ);

        }

    }

    void Update() { 
        RollDice = Input.GetKeyDown(KeyCode.Space);
    }
}
