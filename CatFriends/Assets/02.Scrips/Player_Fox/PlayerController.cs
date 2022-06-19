using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    /// <summary>
    /// ======== Private ========
    /// </summary>
    [SerializeField] private float walkSpeed = 1.5f;  // ======
    [SerializeField] private float runSpeed = 4f;   // 움직임에 필요한 변수들
    [SerializeField] private float rotateSpeed = 5f;// ======
    private float horiz; // Horizontal
    private float verti; // Vertical
    private PlayerAnimator playerAnimator;
    private Vector3 _move; // move Vector
    private Rigidbody rb; 
    private bool isMoveOk; // 움직일 수 있는지 체크
    private float isMoveValue = 0.2f; // 움직일 수 있는 최소 move 벡터 크기


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<PlayerAnimator>();
    }
    private void SetMove(float h, float v, float speed) // 움직임 관련 Setting 함수
    {
        _move.x = h > 0 ? Mathf.Cos(Mathf.Acos(h)) : Mathf.Cos(Mathf.Acos( h /(Mathf.Sqrt((h * h + v * v)))));
        _move.z = v > 0 ? Mathf.Sin(Mathf.Asin(v)) : Mathf.Sin(Mathf.Asin(v / (Mathf.Sqrt((h * h + v * v)))));
        _move = _move * speed;
    }

    private void SetTurn() // 부드럽게 회전하는 함수
    {
        Quaternion newRotation = Quaternion.LookRotation(_move);
        rb.rotation = Quaternion.Slerp(rb.rotation, newRotation, rotateSpeed * Time.fixedDeltaTime);
    }


    private void Update()
    {
        horiz = Input.GetAxis("Horizontal");
        verti = Input.GetAxis("Vertical");

        isMoveOk = Mathf.Sqrt(horiz*horiz + verti*verti) > isMoveValue ? true : false; // horiz,verti의 크기로 결정
        if(isMoveOk)
            playerAnimator.SetBool("IsIdle",false);
        else
            playerAnimator.SetBool("IsIdle", true);

        if (isMoveOk)
        {
            playerAnimator.SetBool("IsWalk", true);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerAnimator.SetBool("IsRun", true); // BlendTree 에서 IsRun 1 로 SET
                SetMove(horiz, verti, runSpeed);
            }
            else
            {
                playerAnimator.SetBool("IsRun", false);
                SetMove(horiz, verti, walkSpeed);
            }
        }
        else // 방향키 x
        {
            playerAnimator.SetBool("IsWalk", false);
        }

        
    }

    private void FixedUpdate()
    {
        if (isMoveOk)
        {
            rb.position += _move * Time.fixedDeltaTime;
            SetTurn();
        }
    }



}
