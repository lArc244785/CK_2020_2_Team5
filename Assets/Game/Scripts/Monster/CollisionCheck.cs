using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    public bool collision;

    // Start is called before the first frame update
    void Start()
    {
        collision = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("forward"))
            return;
        if (other.gameObject.tag == ("Player") || other.gameObject.tag==("Bullet"))
            return;
        if(other.gameObject.tag==("Wall"))
            collision = true;
        collision = true;
        Debug.Log("충돌");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == ("Player") || other.gameObject.tag == ("Bullet"))
            return;
        Debug.Log("충돌끝");
        if (other.gameObject.tag == ("Wall"))
            collision = false;
        collision = false;
        //collision = false;
    }

    public bool isCollision()
    {
        return collision;
    }
}
