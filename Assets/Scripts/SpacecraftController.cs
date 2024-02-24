using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacecraftController : MonoBehaviour
{
    public float thrust = 100;

    public float LX;
    public float LY;
    public float RX;
    public float RY;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        LX = Input.GetAxis("LX");
        LY = -Input.GetAxis("LY");
        RX = Input.GetAxis("RX");
        RY = -Input.GetAxis("RY");

        rb.AddForce(transform.up * thrust * LY);
    }
}
