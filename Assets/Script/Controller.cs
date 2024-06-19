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
        // ���� �پ��ִ��� Ȯ���ϴ� ������ ��
        isGround = Physics2D.OverlapCircle(transform.position, radius, isLayer);
        Debug.Log(isGround);

        animator.SetBool("fly", !isGround);

        // x�� �� �Է¿� ���� �ӵ� ����
        float _horizontal = Input.GetAxisRaw("Horizontal");
        velocity = new Vector2(_horizontal * speed, 0);

        // �̵� ���⿡ ���� �̹��� ȸ��
        if (_horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (_horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        // �����̽��� ������ŭ ���� ����
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
        //�����̽��� ���� ����
        if (Input.GetKeyUp(KeyCode.Space) & isGround)
        {
            animator.SetTrigger("Jumping");
            animator.SetBool("readyJump", false);
            isJump = true;
        }

        //Idle�� Walk ����
        if (velocity.x != 0)
        {
            animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isWalk", false);
        }
    }

    // ����� ���� (���� ������)
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
