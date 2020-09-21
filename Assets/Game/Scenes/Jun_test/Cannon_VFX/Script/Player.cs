using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject player;
    public GameObject cannon;

    public Transform firePos;

    private void Start()
    {
        //Instantiate(cannon, firePos.transform.position, firePos.transform.rotation);
    }
    // Update is called once per frame
    void Update()
    {
        //생성
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(cannon, firePos.transform.position, firePos.transform.rotation);
        }

       // transform.Translate(Vector3.forward * moveSpeed);
    }
}
