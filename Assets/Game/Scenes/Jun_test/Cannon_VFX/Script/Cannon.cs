using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Cannon : MonoBehaviour
{
    public GameObject cannon;

    public float speed =10;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * speed);
        }   
    }
}
