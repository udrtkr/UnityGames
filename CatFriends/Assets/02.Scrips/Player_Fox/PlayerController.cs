using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // todo �ǽ����϶� �ǽ� �÷��̽� �տ� �÷��̽����� �ֱ������� ����� ����, ��� �ൿ �� ������ �ִϸ����� �ߵ�

    /// <summary>
    /// ======== Private ========
    /// </summary>
    [SerializeField] private float walkSpeed = 1.5f;  // ======
    [SerializeField] private float runSpeed = 4f;   // �����ӿ� �ʿ��� ������
    [SerializeField] private float rotateSpeed = 5f;// ======
    private PlayerAnimator playerAnimator;
    private Vector3 _move; // move Vector
    private Rigidbody rb;
    private Collider col;
    private BoxCollider bcol; 
    private float isMoveValue = 0.2f; // ������ �� �ִ� �ּ� move ���� ũ��
    private bool isUnderTheSea;
    private float gravity = -9.81f;
    private float restStartTime = 6f;
    private float horiz // Horizontal
    {
        get { return forceStop ? 0 : Input.GetAxis("Horizontal"); ; }
    }
    private float verti // Vertical
    {
        get { return forceStop ? 0 : Input.GetAxis("Vertical"); }
    }
    private bool isMoveOk // ������ �� �ִ��� üũ horiz,verti�� ũ��� ����  
    {
        get { return Mathf.Sqrt(horiz * horiz + verti * verti) > isMoveValue ? true : false; }
    }

    public bool forceStop;


    [Header("States")]
    public PlyaerState plyaerState;
    public JumpState jumpState;
    


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
        _move.z = v > 0 ? Mathf.Sin(Mathf.Asin(v)) : Mathf.Sin(Mathf.Asin( v / (Mathf.Sqrt((h * h + v * v)))));
        _move = _move * speed;
    }

    private void SetTurn(Quaternion dir) // �ε巴�� ȸ���ϴ� �Լ�
    {
        Quaternion newRotation = dir;
        rb.rotation = Quaternion.Slerp(rb.rotation, newRotation, rotateSpeed * Time.fixedDeltaTime);
    }
    
    public void UnderTheSea(bool TorF)
    {
        playerAnimator.SetBool("IsUnderSea", TorF);
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
        else // ����Ű x
        {
            playerAnimator.SetBool("IsIdle", true);
            playerAnimator.SetBool("IsWalk", false);
            playerAnimator.SetBool("IsRun", false);
            RestStart(restStartTime);
        }

        // PlayerUI 
        PlayerUI.instance.SetGroomingOk(!isMoveOk);
        if (PlayerUI.instance.isGroomingHand ||
            PlayerUI.instance.isFishing ||
            FriendsUI.instance.isTalk) // �׷�� ���϶�, �������϶�, ��ȭ���� ��
        {
            playerAnimator.SetBool("IsIdle", false); // �̶� ������ isIdle false �� 
            playerAnimator.SetBool("IsGrooming", true);
        }
        else
        {
            playerAnimator.SetBool("IsGrooming", false); // �׷�� �ƴ� �� �÷��̾��� Move�� ���� isIdle t/f ����
            
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            PlayerUI.instance.SetGroomingHandOff();
            PlayerUI.instance.SetFishingOff();
        }

        // FriendUI


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
        if (PlayerUI.instance.isGroomingHand || PlayerUI.instance.isFishing) // �׷�� ���϶� or �������϶�
        {
            SetTurn(Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z)); // ������ �ٶ󺸰�
        }
    }

    IEnumerator E_RestStart(float restStartTimer)
    {
        float Timer = restStartTimer;
        while (!isMoveOk)
        {
            if (Timer < 0)
            {
                playerAnimator.SetTrigger("IsRest");
                break;
            }
            else
            {
                Timer -= Time.deltaTime;
                yield return null;
            }
        }
        yield return null;
    }
    private void RestStart(float restStartTimer)
    {
        StartCoroutine(E_RestStart(restStartTimer));
    }


    private void UpdateJumpState()
    {
        switch (jumpState)
        {
            case JumpState.Idle:
                break;
            case JumpState.Prepare:
                // ���� ����
                // Ÿ�̸� ����
                // y �� �ӵ� 0����
                jumpState++;
                break;
            case JumpState.Casting:
                // �׶��忡 ������ ++
                break;
            case JumpState.OnAction:
                // ����� �Ÿ� ��� �� animation ���
                break;
            case JumpState.Finish:
                break;
        }
    }

    public enum JumpState
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish,
    }

}

public enum PlyaerState
{
    Idle,
    Move,
    Jump
}
