using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float runSpeed = 4f;
    private PlayerAnimator playerAnimator;
    private Transform tr;
    private Vector3 _move;
    private Vector3 dir;
    private Rigidbody rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<PlayerAnimator>();
        tr = GetComponent<Transform>();
    }
    private void SetMove(float h, float v, float speed)
    {
        _move.x = h > 0 ? h * Mathf.Cos(Mathf.Acos(h)) * speed : -h * Mathf.Cos(Mathf.Acos(h)) * speed;
        _move.z = v > 0 ? v * Mathf.Sin(Mathf.Asin(v)) * speed : -v * Mathf.Sin(Mathf.Asin(v)) * speed;

        dir = new Vector3(h, 0, v).normalized;

        tr.rotation = Quaternion.LookRotation(dir); // 코루틴으로 다시
    }

    private void Update()
    {
        float horiz = Input.GetAxis("Horizontal");
        float verti = Input.GetAxis("Vertical");

        if(horiz != 0 || verti != 0)
        {
            playerAnimator.SetBool("IsWalk", true);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                SetMove(horiz, verti, runSpeed);
            }
            else
            {
                SetMove(horiz, verti, moveSpeed);
            }
        }
        else
        {
            playerAnimator.SetBool("IsWalk", false);
        }
        
    }

    private void FixedUpdate()
    {
        rb.position += _move * Time.fixedDeltaTime;
    }
}
