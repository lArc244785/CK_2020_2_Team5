using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX_manager : MonoBehaviour
{

    public GameObject cannonBall;
    public Transform firePos;
    public ParticleSystem particle;

    
    public float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(cannonBall, firePos.transform.position, firePos.transform.rotation);
        }

        transform.Translate(Vector3.forward * moveSpeed);
    }
}
