using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendsController : MonoBehaviour
{
    private bool isHello;
    public bool isDeteted;
    public Vector3 playerPos;
    [SerializeField] private float rotateSpeed = 5f;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //rb.rotation = Quaternion.LookRotation(transform.position - new Vector3(0, 3, 0));
    }

    public void Hello(Vector3 dirPlayer)
    {
        transform.LookAt(dirPlayer);
    }

    public void Deteted(bool TorF, Vector3 pos)
    {
        isDeteted = TorF;

        playerPos = pos;
    }
    private void SetTurn(Vector3 dir) // �ε巴�� ȸ���ϴ� �Լ�
    {
        Vector3 _dir = dir - transform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_dir), Time.deltaTime * rotateSpeed);
    }


    private void Update()
    {
        // �÷��̾��ʿ��� ����Ʈ ���� �� �ε巴�� ����
        if (isDeteted)
        {
            SetTurn(new Vector3(playerPos.x, transform.position.y, playerPos.z));
        }
        else
        {
            SetTurn(new Vector3(transform.position.x, transform.position.y, -3));
        }
    }
}
