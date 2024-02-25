using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CasualFlight : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float turnSpeed;
    [SerializeField] float sideRotation;
    [SerializeField] float forwardRotation;
    [SerializeField] float thrust;
    [SerializeField] float thrustM;

    float LX;
    float LY;
    float RX;
    float RY;
    float RT;

    Quaternion turnRotationAngle;
    Quaternion forwardRotationAngle;
    Quaternion sideRotationAngle;

    [Header("Sim")]
    public float _thrust = 0.3f;
    public float torque = 0.1f;

    private int ControllMode = 0;
    float LB;
    float RB;
    public float BUTTONS;
    public float JetMultiplier = 2;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        turnRotationAngle = Quaternion.Euler(0, 0, 0);
        forwardRotationAngle = Quaternion.Euler(0, 0, 0);
        sideRotationAngle = Quaternion.Euler(0, 0, 0);

    }

    void Update()
    {
        Mapping();

        //ManualSimplified();
        //ArcadeCasual();\
        Jet();
    }

    void Mapping()
    {
        LX = (float)Math.Round(Input.GetAxis("LX"), 1);
        LY = (float)Math.Round(-Input.GetAxis("LY"), 1);
        RX = (float)Math.Round(Input.GetAxis("RX"), 1);
        RY = (float)Math.Round(-Input.GetAxis("RY"), 1);
        RT = Input.GetAxis("RT");

        if (Input.GetButton("LB"))
        {
            LB = -1f;
        }
        else
        {
            LB = 0f;
        }
        if (Input.GetButton("RB"))
        {
            RB = 1f;
        }
        else
        {
            RB = 0f;
        }
        BUTTONS = (float)(Math.Round(LB + RB, 1));
    }

    private void ArcadeCasual()
    {
        Vector3 forwardVelocity = transform.right * thrust * (2 + LY);

        //forwardRotationAngle *= Quaternion.Euler(RY * forwardRotation, 0f, 0f);

        sideRotationAngle *= Quaternion.Euler(-RX * sideRotation, 0f, 0f);
        forwardRotationAngle *= Quaternion.Euler(new Vector3(0f, 0f, -RY * forwardRotation));
        turnRotationAngle *= Quaternion.Euler(0f, turnSpeed * LX, 0f);


        //rb.transform.Rotate(new Vector3(RX * forwardRotation, 0f, 0f));
        //rb.transform.Rotate(new Vector3(0f,0f,RY*sideRotation));
        //rb.transform.Rotate(new Vector3(0f, turnSpeed * RX , 0f));

        rb.rotation = turnRotationAngle * forwardRotationAngle * sideRotationAngle;
        rb.velocity = forwardVelocity;
    }
    void ManualSimplified()
    {
        rb.AddForce(transform.right * thrustM * (1 +LY));
        // rb.AddForce(-transform.forward * thrust * LX);
        // rb.AddForce(transform.up * thrust * TRIGGERS);

        // Sideways
        rb.AddTorque(-transform.forward * thrustM * RY * sideRotation * 10f);
        //Up-down
        rb.AddTorque(-transform.right * thrustM * RX * forwardRotation * 1f);
        //Turn
        rb.AddTorque(transform.up * thrustM * LX * turnSpeed);
    }

    void Jet()
    {
        rb.AddForce(transform.right * _thrust * LY * JetMultiplier);
        rb.AddTorque(-transform.forward * torque * RY * forwardRotation);

        //rb.AddTorque(-transform.right * torque * RX);
        rb.AddTorque(-transform.right * torque * BUTTONS * sideRotation);

        //rb.AddTorque(transform.up * sideRotation * BUTTONS);
        rb.AddTorque(transform.up * sideRotation * RX * turnSpeed);


        if (LY > 0.8)
        {
            //Afterburner1.SetActive(true);
            //Afterburner2.SetActive(true);
        }
        else
        {
            //Afterburner1.SetActive(false);
            //Afterburner2.SetActive(false);
        }
    }
}
