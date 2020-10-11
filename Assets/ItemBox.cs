
using System.Collections;

using UnityEngine;

public class ItemBox : MonoBehaviour
{
    private GameObject Modle;
    private GameObject Particle;
    private GameObject Item;

    
    public float randomParcent = 100.0f ;


    // Start is called before the first frame update
    void Start()
    {
        Modle = transform.FindChild("Modle").gameObject;
        Particle = transform.FindChild("Particle").gameObject;
        Item = transform.FindChild("Item").gameObject;

        Item.SetActive(false);
        Particle.SetActive(false);
    }


    private void ItemBoxDestoryEvent()
    {
        StartCoroutine(Evnet());
    }

    IEnumerator Evnet()
    {
        Particle.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        if (Random.Range(.0f, 100.0f) < randomParcent)
        {
            Item.SetActive(true);
            Item.transform.parent = null;
        }

        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            ItemBoxDestoryEvent();
        }
    }
}
