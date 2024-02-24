using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class SpacecraftController : MonoBehaviour
{
    public TMP_Text ControlModeUI;
    public TMP_Text GeneralMessage;

    public float thrust = 0.3f;
    public float torque = 0.1f;

    public int ControllMode = 0;


    public float LX;
    public float LY;
    public float RX;
    public float RY;

    float LB;
    float RB;
    public float BUTTONS;

    float LT;
    float RT;
    public float TRIGGERS;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (ControllMode == 0)
        {
            ControlModeUI.text = "VTOL FULL control";
        }
        else if (ControllMode == 1) 
        {
            ControlModeUI.text = "VTOL ARCADE control";
        }
        else if (ControllMode == 2)
        {
            ControlModeUI.text = "JET control";
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleControls();

        if (rb.position.y > 200 && SceneManager.GetActiveScene().name != "Space")
        {
            SceneManager.LoadScene("Space");
        }
        if (rb.position.y > 100 && SceneManager.GetActiveScene().name != "Space")
        {
            GeneralMessage.text = "Leaving the planet";
        }
        else
        {
            GeneralMessage.text = "";
        }


        

    }

    void HandleControls()
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
        if (Input.GetButton("RB"))
        {
            RB = 1f;
        }
        else
        {
            RB = 0f;
        }
        BUTTONS = (float)(Math.Round(LB + RB, 1));

        LT = -Input.GetAxis("LT");
        RT = Input.GetAxis("RT");

        TRIGGERS = (float)(Math.Round(LT + RT, 1));




        if (Input.GetButtonDown("START"))
        {
            ControllMode++;
            if(ControllMode > 2)
            {
                ControllMode = 0;
            }
            if (ControllMode == 0)
            {
                ControlModeUI.text = "VTOL FULL control";
            }
            else if (ControllMode == 1)
            {
                ControlModeUI.text = "VTOL ARCADE control";
            }
            else if (ControllMode == 2)
            {
                ControlModeUI.text = "JET control";
            }
        }
        if (ControllMode == 0)
        {
            rb.AddForce(transform.right * thrust * LY);
            rb.AddForce(-transform.forward * thrust * LX);
            rb.AddForce(transform.up * thrust * TRIGGERS);

            rb.AddTorque(-transform.forward * torque * RY);
            rb.AddTorque(-transform.right * torque * RX);
            rb.AddTorque(transform.up * torque * BUTTONS);
        }
        else if (ControllMode == 1)
        {
            rb.AddForce(transform.right * thrust * LY);
            rb.AddForce(-transform.forward * thrust * LX);

            rb.AddForce(transform.up * thrust * TRIGGERS);
            rb.AddTorque(transform.up * torque * RX);

        }
        else if (ControllMode == 2)
        {
            rb.AddForce(transform.right * thrust * LY);
            rb.AddTorque(-transform.forward * torque * RY);
            rb.AddTorque(-transform.right * torque * RX);
            rb.AddTorque(transform.up * torque * BUTTONS);

        }
    }
}
