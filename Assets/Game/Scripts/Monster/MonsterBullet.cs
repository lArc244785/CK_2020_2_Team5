using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    public MonsterStatus mstatus;
    Vector3 bulletV;

    void Start()
    {
        bulletV.Set(transform.position.x, transform.position.y, transform.position.z);
    }


    void Update()
    {

    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * mstatus.bulletSpeed * Time.deltaTime);

        //충돌하거나 사거리 끝에 도달하면 없어짐
        if (Vector3.Distance(bulletV, transform.position) > mstatus.longAttackRange)
            Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //플레이어 hp감소
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerControl>().SetDamage(mstatus.longdamage);
            UnityEngine.Debug.Log("공격성공");
            Destroy(gameObject);
        }
        else
        {
            //Destroy(gameObject);
        }

    }
}
