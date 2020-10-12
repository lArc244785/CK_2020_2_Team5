using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    public MonsterStatus mstatus;
    Vector3 bulletV;
    public GameObject hit_effect;

    void Start()
    {
        bulletV.Set(transform.position.x, transform.position.y, transform.position.z);
    }


    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Debug.Log("Code 444: " +mstatus.bulletSpeed);
        transform.Translate(Vector3.forward * mstatus.bulletSpeed * Time.deltaTime);

        //충돌하거나 사거리 끝에 도달하면 없어짐
        if (Vector3.Distance(bulletV, transform.position) > mstatus.longAttackRange)
            Destroy(gameObject);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    //플레이어 hp감소
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        collision.gameObject.GetComponent<PlayerControl>().SetDamage(mstatus.longdamage);
    //        Instantiate(hit_effect, transform.position,transform.rotation);
            
            
    //        UnityEngine.Debug.Log("공격성공");
    //        Destroy(gameObject);

    //    }
    //    else
    //    {
    //        Instantiate(hit_effect, transform);
    //        Destroy(gameObject);
    //    }

    //}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerControl>().SetDamage(mstatus.longdamage);
            Instantiate(hit_effect, transform.position, transform.rotation);


            UnityEngine.Debug.Log("공격성공");
            Destroy(gameObject);

        }
        else 
        {
            Instantiate(hit_effect, transform);
            Destroy(gameObject);
        }
    }
}
