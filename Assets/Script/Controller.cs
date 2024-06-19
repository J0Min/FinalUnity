using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed = 2.0f;
    public float limitjumpSize = 6.0f;
    bool isJump = false;
    public float jumpSize;

    Vector2 velocity;
    Animator animator;
    new Rigidbody2D rigidbody;

    bool isGround = false;
    public LayerMask isLayer;
    public float radius = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 땅에 붙어있는지 확인하는 오버랩 원
        isGround = Physics2D.OverlapCircle(transform.position, radius, isLayer);
        Debug.Log(isGround);

        animator.SetBool("fly", !isGround);

        // x축 값 입력에 따른 속도 조절
        float _horizontal = Input.GetAxisRaw("Horizontal");
        velocity = new Vector2(_horizontal * speed, 0);

        // 이동 방향에 따른 이미지 회전
        if (_horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (_horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        // 스페이스바 누른만큼 높게 점프
        if (Input.GetKey(KeyCode.Space) & isGround)
        {
            animator.SetBool("readyJump", true);
            if (jumpSize < limitjumpSize)
            {
                jumpSize += (Time.deltaTime*4);
            }
            else if (jumpSize > limitjumpSize)
            {
                jumpSize = limitjumpSize;
            }
        }
        //스페이스바 떼면 점프
        if (Input.GetKeyUp(KeyCode.Space) & isGround)
        {
            animator.SetTrigger("Jumping");
            animator.SetBool("readyJump", false);
            isJump = true;
        }

        //Idle과 Walk 제어
        if (velocity.x != 0)
        {
            animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isWalk", false);
        }
    }

    // 기즈모 생성 (보기 쉬우라고)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }


    void FixedUpdate()
    {
        if (isJump == false)
        {
            rigidbody.velocity = new Vector2(velocity.x, rigidbody.velocity.y);
        }
        else
        {
            //rigidbody.velocity = new Vector2(velocity.x, rigidbody.velocity.y + jumpSize);
            rigidbody.AddForce(Vector2.up * jumpSize, ForceMode2D.Impulse);
            isJump = false;
        }
    }

}
