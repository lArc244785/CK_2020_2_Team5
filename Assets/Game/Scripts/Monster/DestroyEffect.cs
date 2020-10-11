using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] objects;
    ParticleSystem[] paticles;
    int object_num;
    void Start()
    {
        object_num = objects.Length;
        paticles = new ParticleSystem[object_num];
        for(int i = 0; i < object_num; i++)
        {
            paticles[i] = objects[i].GetComponent<ParticleSystem>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < object_num; i++)
        {
            if (paticles[i]!=null && !paticles[i].IsAlive())
            {
                Destroy(paticles[i]);
                Destroy(objects[i]);
            }
        }

        if (paticles[0] == null)
        {
            Destroy(this.gameObject);
        }
    }

}
