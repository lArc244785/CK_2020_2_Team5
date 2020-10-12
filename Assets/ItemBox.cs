
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
        Modle = transform.Find("Modle").gameObject;
        Particle = transform.Find("Particle").gameObject;
        Item = transform.Find("Item").gameObject;

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
        Modle.SetActive(false);
        //yield return new WaitForSeconds(0.2f);
        if (Random.Range(.0f, 100.0f) < randomParcent)
        {
            Item.SetActive(true);
            RandomScroll();
            Item.transform.parent = null;
        }
        yield return new WaitForSeconds(0.6f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            ItemBoxDestoryEvent();
        }
    }

    private void RandomScroll()
    {
        PaperItem paper = Item.GetComponent<PaperItem>();
        if (paper == null) return;

        int r = Random.Range(0, 3);

        paper.item = (EnumInfo.Item)r;

    }



}
