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

    public void SetRotatePlayer()
    {
        transform.LookAt(new Vector3(playerPos.x, transform.position.y, playerPos.z)); // ���� ��ã���� �ٽ�
        //Quaternion newRotation = Quaternion.LookRotation(playerPos);
        //rb.rotation = Quaternion.Slerp(rb.rotation, newRotation, rotateSpeed * Time.fixedDeltaTime);
    }
    private void SetRotateOriginal()
    {
        transform.LookAt(transform.position - new Vector3(0, 0, 3));
        //Quaternion newRotation = Quaternion.LookRotation(transform.position - new Vector3(0, 0, 3));
        //rb.rotation = Quaternion.Slerp(rb.rotation, newRotation, rotateSpeed * Time.fixedDeltaTime);
    }

    

    private void FixedUpdate()
    {
        // �÷��̾��ʿ��� ����Ʈ ���� �� �ε巴�� ����
        if (isDeteted)
        {
            SetRotatePlayer();
        }
        else
        {
            SetRotateOriginal();
        }
    }
}
