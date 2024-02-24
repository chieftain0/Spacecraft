using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SpacecraftController : MonoBehaviour
{
    public TMP_Text ControlModeUI;



    public float thrust = 0.3f;
    public float torque = 0.1f;

    public bool ManualControllMode = false;


    public float LX;
    public float LY;
    public float RX;
    public float RY;

    public float LB;
    public float RB;
    public float BUTTONS;

    public float LT;
    public float RT;
    public float TRIGGERS;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        LX = (float)(Math.Round(Input.GetAxis("LX"), 1));
        LY = (float)(Math.Round(-Input.GetAxis("LY"), 1));
        RX = (float)(Math.Round(Input.GetAxis("RX"), 1));
        RY = (float)(Math.Round(-Input.GetAxis("RY"), 1));

        if (Input.GetButton("LB"))
        {
            LB = -1f;
        }
        else
        {
            LB = 0f;
        }
        if(Input.GetButton("RB"))
        {
            RB = 1f;
        }
        else
        {
            RB = 0f;
        }
        BUTTONS = LB + RB;

        LT = -Input.GetAxis("LT");
        RT = Input.GetAxis("RT");

        TRIGGERS = LT + RT;


        if(Input.GetButtonDown("START"))
        {
            ManualControllMode = !ManualControllMode;
        }
        if (ManualControllMode)
        {
            ControlModeUI.text = "MANUAL control";
        }
        else
        {
            ControlModeUI.text = "ARCADE control";
        }


        if (ManualControllMode == true)
        {
            rb.AddRelativeForce(transform.right * thrust * LY);
            rb.AddRelativeForce(-transform.forward * thrust * LX);
            rb.AddRelativeForce(transform.up * thrust * TRIGGERS);

            rb.AddRelativeTorque(-transform.forward * torque * RY);
            rb.AddRelativeTorque(-transform.right * torque * RX);
            rb.AddRelativeTorque(transform.up * torque * BUTTONS);
        }
        else
        {

        }

    }
}
