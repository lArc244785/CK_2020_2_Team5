﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject player;
    public GameObject cannon;

    public Transform firePos;

    public GameObject effect;

    Vector3 shoot;

    private void Start()
    {
        //Instantiate(cannon, firePos.transform.position, firePos.transform.rotation);
        Vector3 shoot = firePos.position;
    }
    // Update is called once per frame
    void Update()
    {
        //생성
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(cannon, firePos.transform.position, firePos.transform.rotation);

            GameObject cannonEffect = Instantiate(effect, firePos.transform.position, firePos.transform.rotation);

            Destroy(cannonEffect,2f) ;
        }
    }
}
