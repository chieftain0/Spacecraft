using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<SimpleShooting>().GotHit(5f);
        }
    }
}
