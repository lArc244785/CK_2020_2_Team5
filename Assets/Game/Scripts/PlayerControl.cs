using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Range(0,50)]
    public float playerMoveSpeed = 5;
    public float playerRotaSpeed = 10;

    Quaternion playerRotation;
    private Rigidbody rb;
    Vector3 pmove;

    float phorizon;
    float pvertical;

    //총알
    public GameObject Bullet;
    public Transform FirePos;
    int maxbullet = 5; //최대총알수 등은 나중에 다른 스크립트로 이동 예정
    int getbullet = 0;
    float reloadTime = 2f;
    float reTime = 0;
    bool load;

    //대쉬중인가 / 대쉬 쿨 / 대쉬 시 속도
    bool dash;
    float dashTime = 0f;
    public float dashCool = 10f; //임시
    float dashCoolTime = 10;
    public float dashSpeed = 8f; //임시

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        getbullet = maxbullet;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayerAttack();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dash();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReLoad();
        }
    }

    private void FixedUpdate()
    {
        phorizon = Input.GetAxisRaw("Horizontal");
        pvertical = Input.GetAxisRaw("Vertical");
        dashCoolTime += 0.1f;
        PlayerMove();
        PlayerTurn();
    }

    void PlayerMove()
    {
        if (dash == false) //대쉬중일땐 움직이지 못함
        {
            pmove.Set(phorizon, 0.0f, pvertical);
            pmove = pmove.normalized * playerMoveSpeed * Time.deltaTime;
            rb.MovePosition(transform.position + pmove);
        }
    }

    void PlayerTurn()
    {
        if (phorizon == 0 && pvertical == 0)
            return;
        playerRotation = Quaternion.LookRotation(pmove);
        rb.rotation = Quaternion.Slerp(rb.rotation, playerRotation, playerRotaSpeed * Time.deltaTime);
    }

    void PlayerAttack() //마우스 좌클릭 시 포탄 발사
    {
        if (getbullet > 0)
        {
            Instantiate(Bullet, FirePos.transform.position, FirePos.transform.rotation);
            getbullet -= 1;
            UnityEngine.Debug.Log("현재 총알 : " + getbullet.ToString());
            if (getbullet == 0)
                ReLoad();
        }
        
    }

    void Dash()
    {
        //시간재는 함수 추가
        if (dash == false)
        {
            if (dashCool > dashCoolTime)
                return;
            else
            {
                dash = true;
                StartCoroutine(GoDash());
            }
        }
    }

    IEnumerator GoDash()
    {
        dashTime += 0.1f;
        if (dash == true && 5f >= dashTime)
        {
            transform.Translate(Vector3.forward * 2.5f * Time.deltaTime);
            StartCoroutine(GoDash());
        }
        else
        {
            UnityEngine.Debug.Log("대시종료");
            dashCoolTime = 0f;
            dash = false;
            dashTime = 0f;
            yield return null;
        }
    }

    void ReLoad()
    {
        if (maxbullet == getbullet && load==true)
            return;
        else
        {
            UnityEngine.Debug.Log("장전시작");
            load = true;
            StartCoroutine(ReLoading());
        }

    }

    IEnumerator ReLoading()
    {
        yield return new WaitForSeconds(reloadTime);
        load = false;
        getbullet = maxbullet;
        UnityEngine.Debug.Log("장전끝 현재 총알 :"+getbullet.ToString());
    }
}
