using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    float bulletSpeed=15f;
    float bulletRange=15f;

    Vector3 bulletV;

    void Start()
    {
        bulletV.Set(transform.position.x, transform.position.y, transform.position.z);
    }


    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);

        //충돌하거나 사거리 끝에 도달하면 없어짐
        if (Vector3.Distance(bulletV, transform.position) > bulletRange)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //몬스터면 맞은 몬스터의 hp감소
        Destroy(gameObject);
    }
}
