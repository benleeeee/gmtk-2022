using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dice : MonoBehaviour
{
    private float LifeSpan = 2.0f;
    private float StartTime;
    private bool HasRoll = false;

    public int RollValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartTime = Time.time;
        gameObject.transform.position = new Vector3(0, 0, -3);
}

    // Update is called once per frame

    private void FixedUpdate()
    {


    }

    void Update() {
        if (Time.time - StartTime >= LifeSpan)
        {
            if (!HasRoll)
            {
                GenerateNumber();
            }
            Destroy(gameObject);
        }
    }

    public void GenerateNumber()
    {
        RollValue = (int)Random.Range(1, 7);
        HasRoll = true;
    }
}
