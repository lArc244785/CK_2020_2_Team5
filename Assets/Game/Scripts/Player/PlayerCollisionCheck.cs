using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionCheck : MonoBehaviour
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
        if (other.gameObject.tag == ("Player") || other.gameObject.tag == ("Bullet") || other.gameObject.tag =="Box" )
            return;

        collision = true;
        Debug.Log("충돌");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == ("Player") || other.gameObject.tag == ("Bullet") || other.gameObject.tag == "Box")
            return;
        Debug.Log("충돌끝");
        collision = false;
    }

    public bool isCollision()
    {
        return collision;
    }
}
