using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Range(0,50)]
    public float playerMoveSpeed = 5;

    public PlayerStatus playerStatus;

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
    bool load;

    //대쉬중인가 / 대쉬 쿨 / 대쉬 시 속도
    bool isdash;
    float dTime = 0f;
    public float dashCoolTimeMax = 4f; //임시
    float dashCoolTime;
    public float dashSpeed = 8f; //임시

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        getbullet = maxbullet;
        dashCoolTime = dashCoolTimeMax;
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
        dashCoolTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        phorizon = Input.GetAxisRaw("Horizontal");
        pvertical = Input.GetAxisRaw("Vertical");
        PlayerMove();
        PlayerTurn();
    }

    void PlayerMove()
    {
        if (isdash == false) //대쉬중일땐 움직이지 못함
        {
            pmove.Set(phorizon, 0.0f, pvertical);
            pmove = pmove.normalized * playerMoveSpeed * Time.deltaTime;
            rb.MovePosition(transform.position + pmove);
        }
    }

    //아직 이해를 못한 부분입니다. =========================
    void PlayerTurn()
    {
        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(b.x - a.x, b.y - a.y) * Mathf.Rad2Deg ;
    }
    //=======================

    void PlayerAttack() //마우스 좌클릭 시 포탄 발사
    {
        if (getbullet > 0)
        {
            Instantiate(Bullet, FirePos.transform.position, FirePos.transform.rotation);
            getbullet -= 1;
            //UnityEngine.Debug.Log("현재 총알 : " + getbullet.ToString());
            if (getbullet == 0)
                ReLoad();
        }
        
    }

    void Dash()
    {
        if (isdash == false)
        {
            if (dashCoolTimeMax > dashCoolTime)
                return;
            else
            {
                isdash = true;
                StartCoroutine(GoDash());
            }
        }
    }

    IEnumerator GoDash()
    {
        while (true)
        {
            dTime += Time.deltaTime;
            if (isdash == true && 1f >= dTime)
            {
                transform.Translate(Vector3.forward * 2.5f * Time.deltaTime);
            }
            else
            {
                dashCoolTime = 0f;
                isdash = false;
                dTime = 0f;
                yield return null;
                break;
            }
        }

    }

    void ReLoad()
    {
        if (maxbullet == getbullet && load==true)
            return;
        else
        {
            //UnityEngine.Debug.Log("장전시작");
            load = true;
            StartCoroutine(ReLoading());
        }

    }

    IEnumerator ReLoading()
    {
        yield return new WaitForSeconds(reloadTime);
        load = false;
        getbullet = maxbullet;
        //UnityEngine.Debug.Log("장전끝 현재 총알 :"+getbullet.ToString());
    }

    public int getBullet()
    {
        return getbullet;
    }

    public int getMaxBullet()
    {
        return maxbullet;
    }

    public bool getIsReload()
    {
        return load;
    }

    
    public Vector2 getDashTime()
    {
        //현재 대쉬 쿨타임이랑, 총 대쉬 쿨탐
        return new Vector2(dashCoolTime,  5.0f);
    }

    public void SetDamage(float damage)
    {
        playerStatus.hp -= (int)damage;
        if (playerStatus.hp <= 0)
        {
            Debug.Log("GameOver");
        }
    }
}
