using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenObject : MonoBehaviour
{
    public int hit_count;
    int hit=0;
    public GameObject effect;
    //아이템을 떨구는 오브젝트인가
    public bool drop_item = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            hit++;
            if (hit >= hit_count)
            {
                //효과재생
                //drop_item이 true이면 아이템 생성
                gameObject.SetActive(false);
            }
            
        }
    }

    //확률을 돌려서 아이템 생성
    void Random_Item()
    {

    }
}
