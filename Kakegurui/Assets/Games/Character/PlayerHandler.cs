using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHandler : MonoBehaviour
{
    public bool isMovable;
    private float moveSpeed = 1.5f;
    AnimatorSetting aniSetting;
    // Start is called before the first frame update
    private void Awake()
    {
        aniSetting = GetComponent<AnimatorSetting>();
    }
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "0_Hall") // Ȧ������ ������ �� ����
            isMovable = true;
        else
            isMovable = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (isMovable) // ������ �� ���� ���� ����Ű �Է� ����, ������ ���� Ȧ������ Ű���� ���������� �ִϸ����� ����, �����忡���� Ű����x ���� ���п� ���� �Ŵ����� ���� ����
        {
            float horizon = Input.GetAxis("Horizontal");
        }
        */
    }

    private void FixedUpdate()
    {
        if (isMovable) // ������ �� ���� ���� ����Ű �Է� ����, ������ ���� Ȧ������ Ű���� ���������� �ִϸ����� ����, �����忡���� Ű����x ���� ���п� ���� �Ŵ����� ���� ����
        {
            float horizon = Input.GetAxis("Horizontal");
            PlayerMove(horizon, Time.fixedDeltaTime);
        }
    }

    private void PlayerMove(float h, float t) // horizontal, deltaTime
    {
        if(Mathf.Abs(h) >= 0.2) // �ּ� �Է°�, �ε巯�� �Է¿� ���� ������ ����
        {
            if (!aniSetting.GetBool("isWalk")) // isWalk�� false�̸� true�� �ٲ���, �ȴ� �ִϸ��̼����� ����
            {
                aniSetting.SetBool("isWalk", true);
                // ������ȯ�� �ѹ��� �ҷ���
                if(h > 0)
                    transform.eulerAngles = new Vector3(0, 90, 0);
                else
                    transform.eulerAngles = new Vector3(0, -90, 0);
            }
            if(h > 0) // ���, x > 0���� ������ ��
            {
                transform.position += new Vector3(1, 0, 0) * moveSpeed * t;
            }
            else // ����, x < 0 ���� ������ ��
            {
                transform.position += new Vector3(-1, 0, 0) * moveSpeed * t;
            }
            
        }
        else // �ּ� �Էº��� ���� ��, �� ������ ���� ��, �ִϸ��̼� Idle, 
        {
            if (aniSetting.GetBool("isWalk")) // isWalk�� true�̸� false�� �ٲ���, Idle �ִϸ��̼����� ���ƿ�
            {
                aniSetting.SetBool("isWalk", false);
                transform.eulerAngles = Vector3.zero;
            }
        }
    }
}
