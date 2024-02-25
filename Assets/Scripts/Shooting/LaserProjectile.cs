using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    public Vector3 startVelocity;
    public Vector3 initialPos;
    void Start()
    {
        initialPos = transform.position;
    }

    void Update()
    {
        transform.position += (startVelocity * Time.deltaTime);

        if((transform.position - initialPos).magnitude > 200f)
            Destroy(gameObject);
    }
}
