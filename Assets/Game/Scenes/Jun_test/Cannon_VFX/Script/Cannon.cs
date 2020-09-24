using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Cannon : MonoBehaviour
{
    public GameObject cannon;

    //public GameObject effect1;
    //public GameObject effect2;
    //public GameObject effect3;

    public float speed =10;

    private void Start()
    {
        GetComponent<Rigidbody>().useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * speed);

    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("충돌");
        GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * 0);
        //GameObject cannonEffect = Instantiate(effect1, cannon.transform.position, cannon.transform.rotation);
        //Destroy(effect1)
        //Destroy(effect2);
        // Destroy(effect3);

        Destroy(cannon, 1f);
    }
}
