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
    private PlayerAnimator playerAnimator;
    private Vector3 _move; // move Vector
    private Rigidbody rb;
    private Collider col;
    private BoxCollider bcol; 
    private float isMoveValue = 0.2f; // 움직일 수 있는 최소 move 벡터 크기
    public bool isUnderTheSea;
    private float gravity = -9.81f;
    public bool forceStop;
    private float horiz // Horizontal
    {
        get { return forceStop ? 0 : Input.GetAxis("Horizontal"); ; }
    }
    private float verti // Vertical
    {
        get { return forceStop ? 0 : Input.GetAxis("Vertical"); }
    }
    private bool isMoveOk // 움직일 수 있는지 체크 horiz,verti의 크기로 결정  
    {
        get { return Mathf.Sqrt(horiz * horiz + verti * verti) > isMoveValue ? true : false; }
    }



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<PlayerAnimator>();
        col = GetComponent<Collider>();
        bcol = GetComponent<BoxCollider>();
    }

    private void Gravity(float g)
    {
        rb.AddForce(new Vector3(0, g * rb.mass, 0), ForceMode.Acceleration);
    }
    private void SetMove(float h, float v, float speed) // 움직임 관련 Setting 함수
    {
        _move.x = h > 0 ? Mathf.Cos(Mathf.Acos(h)) : Mathf.Cos(Mathf.Acos( h /(Mathf.Sqrt((h * h + v * v)))));
        _move.z = v > 0 ? Mathf.Sin(Mathf.Asin(v)) : Mathf.Sin(Mathf.Asin( v / (Mathf.Sqrt((h * h + v * v)))));
        _move = _move * speed;
    }

    private void SetTurn(Quaternion dir) // 부드럽게 회전하는 함수
    {
        Quaternion newRotation = dir;
        rb.rotation = Quaternion.Slerp(rb.rotation, newRotation, rotateSpeed * Time.fixedDeltaTime);
    }
    
    public void UnderTheSea(bool TorF)
    {
        playerAnimator.SetBool("IsUnderSea", TorF);
    }

    public void ForceStop(bool stop)
    {
        forceStop = stop;
    }
    
    public void WaitAniTime()
    {
        float Timer = playerAnimator.GetCurrentAniTime();
        E_WaitAniTime(Timer);
        ForceStop(false); // 강제멈춤 종료
    }

    private IEnumerator E_WaitAniTime(float Timer)
    {
        yield return new WaitForSeconds(Timer);
    }
    
    private void Update()
    {
        if (isMoveOk)
        {
            playerAnimator.SetBool("IsIdle", false);
            playerAnimator.SetBool("IsWalk", true);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerAnimator.SetBool("IsRun", true); 
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
            playerAnimator.SetBool("IsIdle", true);
            playerAnimator.SetBool("IsWalk", false);
            playerAnimator.SetBool("IsRun", false);
        }

        // PlayerUI 
        PlayerUI.instance.SetGroomingOk(!isMoveOk);
        if (PlayerUI.instance.GetGrooming()) // 그루밍 중일때
        {
            forceStop = true;
            playerAnimator.SetBool("IsIdle", false); // 이땐 강제로 isIdle false 로 
            playerAnimator.SetBool("IsGrooming", true);
        }
        else
        {
            //forceStop = false;
            playerAnimator.SetBool("IsGrooming", false); // 그루밍 아닐 땐 플레이어의 Move에 따라 isIdle t/f 결정
            WaitAniTime(); // 현재 애니메이션 타임 기다리고 움직임
        }

        // FriendUI
        if (FriendsUI.instance.isTalk)
            forceStop = true;
        else
            forceStop = false;

    }

    

    private void FixedUpdate()
    {
        Gravity(gravity);

        if (isMoveOk)
        {
            rb.position += _move * Time.fixedDeltaTime;
            SetTurn(Quaternion.LookRotation(_move));
        }

        // PlayerUI 
        if (PlayerUI.instance.GetGrooming()) // 그루밍 중일때
        {
            SetTurn(Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z)); // 앞쪽을 바라보게
        }
    }



}
