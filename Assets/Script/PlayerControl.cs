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
    float bulletRange = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayerAttack();
        }
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
        pmove.Set(phorizon, 0.0f, pvertical);
        pmove = pmove.normalized * playerMoveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + pmove);
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
        Instantiate(Bullet, FirePos.transform.position, FirePos.transform.rotation);
    }
}
