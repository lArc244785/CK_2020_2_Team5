using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Cannon : MonoBehaviour
{
    public GameObject cannon;

    public ParticleSystem effect1;
    public ParticleSystem effect2;
    public ParticleSystem effect3;
    public ParticleSystem effect4;

    public float speed =10;

    private void Awake()
    {
        //대포알 충돌 후 낙하 구현
        //GetComponent<Rigidbody>().useGravity = false;
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("충돌");

        effect1.Stop();
        effect2.Stop();
        effect3.Stop();
        effect4.Stop();

        effect1.transform.parent = null;
        effect2.transform.parent = null;
        effect3.transform.parent = null;
        effect4.transform.parent = null;

        Destroy(cannon);
        Destroy(effect1, 2f);
        Destroy(effect2, 2f);
        Destroy(effect3, 2f);
        Destroy(effect4, 2f);
        //GetComponent<Rigidbody>().useGravity = true;

    }
}
