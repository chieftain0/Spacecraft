using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTheObject : MonoBehaviour
{
    public float rotationSpeed = 10f; // Speed of rotation in degrees per second

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around its up axis (y-axis) by the specified speed
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
