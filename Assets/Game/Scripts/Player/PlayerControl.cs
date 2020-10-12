using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject collisionFoward;

    public PlayerStatus playerStatus;

    private Rigidbody rb;
    Vector3 pmove;

    float attack_tick;
    
    float phorizon;
    float pvertical;

    //총알
    public GameObject Bullet;
    public Transform FirePos;
    //int maxbullet = 5; //최대총알수 등은 나중에 다른 스크립트로 이동 예정
    //int getbullet = 0;
    //float reloadTime = 2f;
    bool load;

    //대쉬중인가 / 대쉬 쿨 / 대쉬 시 속도
    bool isdash;
    float dTime = 0f;
    public float dashCoolTimeMax = 4f; //임시
    float dashCoolTime;
    public float dashSpeed = 8f; //임시
    bool dashStop;
    //================애니메이션 관련=======
    public Animator playeranim;
    bool isHit=false;                    //맞았는지
    bool isMoving;                 //움직이는 중인지
                                   //======================================


    //=============이펙트==================
    public GameObject fireCannon;
    public GameObject hitSword;
    public Transform hitSwordTransform;
    public GameObject hitArrow;
    public Transform hitArrowTransform;

    //=====================================

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dashCoolTime = dashCoolTimeMax;
        playeranim.SetTrigger("idle");
        dashStop = false;
        playerStatus.isLive = true;
        attack_tick = 0;
    }

    void Update()
    {
        IsCollision();
        if (playerStatus.isLive == true)
        {
            if (GameManger.instance.GetGameState() == EnumInfo.GameState.Ingame)
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

            if (playeranim.GetCurrentAnimatorStateInfo(0).IsName("hit"))
            {
                Ishit();
            }
            attack_tick += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (playerStatus.isLive == true)
        {
            if (GameManger.instance.GetGameState() == EnumInfo.GameState.Ingame)
            {
            phorizon = Input.GetAxisRaw("Horizontal");
            pvertical = Input.GetAxisRaw("Vertical");
            PlayerMove();
            PlayerTurn();
            playerAnimation();
            }
        }
    }

    void PlayerMove()
    {
        if (isdash == false) //대쉬중일땐 움직이지 못함
        {
            if (playerStatus.isLive == false)
                return;

            if ((Input.GetButton("Horizontal") || Input.GetButton("Vertical"))||(Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")))
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }

            pmove.Set(phorizon, 0.0f, pvertical);
            pmove = pmove.normalized * playerStatus.moveSpeed * Time.deltaTime;
            rb.MovePosition(transform.position + pmove);
        }
    }

    void playerAnimation()
    {
        if (!playeranim.GetCurrentAnimatorStateInfo(0).IsName("run") && isMoving == true && isHit==false)
        {
            Debug.Log("움직이려고 함");
            playeranim.SetTrigger("run");
        }
        else if (!playeranim.GetCurrentAnimatorStateInfo(0).IsName("idle") && isMoving == false && isHit == false)
        {
            playeranim.SetTrigger("idle");
            Debug.Log("멈춰있다");
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

        if (attack_tick >= playerStatus.attack_tickRate)
        {
            if (playerStatus.getBullet > 0)
            {
                Instantiate(fireCannon, FirePos.transform.position, FirePos.transform.rotation);
                Instantiate(Bullet, FirePos.transform.position, FirePos.transform.rotation);
                playerStatus.getBullet -= 1;

                playeranim.SetTrigger("attack");

                attack_tick = 0;
                GameManger.instance.getInGameUIManger().getcanonUI().ShootBullet(playerStatus.getBullet);
                //UnityEngine.Debug.Log("현재 총알 : " + getbullet.ToString());
                if (playerStatus.getBullet == 0)
                    ReLoad();

                //잘 안사라짐
                Destroy(fireCannon, 1);
            }
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
            if (dashStop == true)
            {
                Debug.Log("대시 중단");
                dashCoolTime = 0f;
                isdash = false;
                dTime = 0f;
                break;
            }

            dTime += Time.deltaTime;

            if (isdash == true && 1f >= dTime)
            {
                float dash_f = Time.deltaTime * 1f;
                transform.Translate(Vector3.forward*dash_f);
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
        if (playerStatus.maxBullet == playerStatus.getBullet && load==true)
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
        //UI리로드 이벤트를 실행시킵니다.
        GameManger.instance.getInGameUIManger().getcanonUI().ReLoadEvent(playerStatus.reLoadTime);
        yield return new WaitForSeconds(playerStatus.reLoadTime);
        load = false;
        playerStatus.getBullet = playerStatus.maxBullet;
        //UnityEngine.Debug.Log("장전끝 현재 총알 :"+getbullet.ToString());
    }

    public int getBullet()
    {
        return playerStatus.getBullet;
    }

    public int getMaxBullet()
    {
        return playerStatus.maxBullet;
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


    public void GetDamageForSword(float damage)
    {
        if (playerStatus.isLive == true)
        {
            playerStatus.hp -= (int)damage;
            if (playerStatus.hp <= 0)
            {
                playeranim.SetTrigger("die");
                playerStatus.isLive = false;
                Debug.Log("GameOver");
                GameManger.instance.getInGameUIManger().GetGameOverUI().GameOver();
            }
            playeranim.SetTrigger("hit");
            isHit = true;
            Instantiate(hitSword, hitSwordTransform.position, hitSwordTransform.rotation);

            Debug.Log("데미지!!!");
            SetHpUI();


        }
    }

    public void GetDamageForArrow(float damage)
    {
        if (playerStatus.isLive == true)
        {
            playerStatus.hp -= (int)damage;
            if (playerStatus.hp <= 0)
            {
                playeranim.SetTrigger("die");
                playerStatus.isLive = false;
                Debug.Log("GameOver");
                GameManger.instance.getInGameUIManger().GetGameOverUI().GameOver();
            }
            playeranim.SetTrigger("hit");
            isHit = true;
            Instantiate(hitArrow, hitArrowTransform.position, hitArrowTransform.rotation);

            Debug.Log("데미지!!!");
            SetHpUI();
        }
    }

    public void SetHpUI()
    {
        GameManger.instance.getInGameUIManger().GetHpUI().SetHp(playerStatus.hp);
    }


    public void AddHP(int hp)
    {
        if(playerStatus.maxHp >= playerStatus.hp + hp)
        {
            playerStatus.hp += hp;
        }
        else
        {
            playerStatus.hp = playerStatus.maxHp;
        }
        SetHpUI();
    }


    void IsCollision()
    {
        playerStatus.fCollision = collisionFoward.GetComponent<PlayerCollisionCheck>().isCollision();
        if (playerStatus.fCollision == true)
        {
            dashStop = true;
        }
        else
        {
            dashStop = false;
        }
    }

    void Ishit()
    {
        if (playeranim.GetCurrentAnimatorStateInfo(0).IsName("hit"))
        {
            if (playeranim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                isHit = false;
            }
        }
    }

    public void StatUp_AttackRange()
    {
        playerStatus.attackRangeUp += 0.5f;
        Debug.Log("사거리증가");
    }

    public void StatUp_PowerUp()
    {
        playerStatus.attackPower += 0.1f;
    }

    public void StatUp_ReLoadUp()
    {
        playerStatus.reLoadTime -= 0.1f;
    }

    public void StatUp_SpeedUp()
    {
        playerStatus.moveSpeed += 1;
    }
    //hit일때 끝났음을 확인하는 코드를 추가하고, bool값이 true이면 다른게 움직일 수 있도록? -> hit상태에서 hit을 부르는건 그대로 부를 수 있도록 조절
}
