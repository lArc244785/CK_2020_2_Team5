using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortAttackRange : MonoBehaviour
{
    public bool can_attack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            can_attack = true;
        }
        else
        {
            can_attack = false;
        }
    }

    public bool Set_canAttack()
    {
        return can_attack;
    }
}
