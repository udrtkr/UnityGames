using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    /// <summary>
    /// ======== Private ========
    /// </summary>
    [SerializeField] private float walkSpeed = 1.5f;  // ======
    [SerializeField] private float runSpeed = 4f;   // �����ӿ� �ʿ��� ������
    [SerializeField] private float rotateSpeed = 5f;// ======
    private float horiz; // Horizontal
    private float verti; // Vertical
    private PlayerAnimator playerAnimator;
    private Vector3 _move; // move Vector
    private Rigidbody rb;
    private Collider col;
    private BoxCollider bcol;
    private bool isMoveOk; // ������ �� �ִ��� üũ
    private float isMoveValue = 0.2f; // ������ �� �ִ� �ּ� move ���� ũ��
    public bool isUnderTheSea;
    private float waterDensity = 0.9998f;
    private float gravity = -9.81f;
    private bool forceStop;



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
    private void SetMove(float h, float v, float speed) // ������ ���� Setting �Լ�
    {
        _move.x = h > 0 ? Mathf.Cos(Mathf.Acos(h)) : Mathf.Cos(Mathf.Acos( h /(Mathf.Sqrt((h * h + v * v)))));
        _move.z = v > 0 ? Mathf.Sin(Mathf.Asin(v)) : Mathf.Sin(Mathf.Asin(v / (Mathf.Sqrt((h * h + v * v)))));
        _move = _move * speed;
    }

    

    private void SetTurn() // �ε巴�� ȸ���ϴ� �Լ�
    {
        Quaternion newRotation = Quaternion.LookRotation(_move);
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

    
    private void Update()
    {
        if (!forceStop) // ������ ������ x
        {
            horiz = Input.GetAxis("Horizontal");
            verti = Input.GetAxis("Vertical");
        }
        else // ������ ����
        {
            horiz = 0;
            verti = 0;
        }

        isMoveOk = Mathf.Sqrt(horiz*horiz + verti*verti) > isMoveValue ? true : false; // horiz,verti�� ũ��� ����    

        if (isMoveOk)
        {
            playerAnimator.SetBool("IsIdle", false);
            playerAnimator.SetBool("IsWalk", true);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerAnimator.SetBool("IsRun", true); // BlendTree ���� IsRun 1 �� SET
                SetMove(horiz, verti, runSpeed);
            }
            else
            {
                playerAnimator.SetBool("IsRun", false);
                SetMove(horiz, verti, walkSpeed);
            }
        }
        else // ����Ű x
        {
            playerAnimator.SetBool("IsIdle", true);
            playerAnimator.SetBool("IsWalk", false);
            playerAnimator.SetBool("IsRun", false);
        }

        // PlayerUI 
        PlayerUI.instance.SetGroomingOk(!isMoveOk);
        if (PlayerUI.instance.GetGrooming()) // �׷�� ���϶�
        {
            forceStop = true;
            playerAnimator.SetBool("IsIdle", false); // �̶� ������ isIdle false �� 
            playerAnimator.SetBool("IsGrooming", true);
        }
        else
        {
            forceStop = false;
            playerAnimator.SetBool("IsGrooming", false); // �׷�� �ƴ� �� �÷��̾��� Move�� ���� isIdle t/f ����
        }

    }

    

    private void FixedUpdate()
    {
        Gravity(gravity);

        if (isMoveOk)
        {
            rb.position += _move * Time.fixedDeltaTime;
            SetTurn();
        }

        // PlayerUI 
        if (PlayerUI.instance.isGroomingHand) // �׷�� ���϶�
        {
            rb.rotation = Quaternion.Slerp(rb.rotation,
                                            Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z),
                                            rotateSpeed * Time.fixedDeltaTime);
        }
    }



}
