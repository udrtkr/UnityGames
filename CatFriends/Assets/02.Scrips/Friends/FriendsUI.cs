using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendsUI : MonoBehaviour
{
    public static FriendsUI instance;

    public FrirendInfo friendInfo;

    [SerializeField] private float height = 1f;
    [SerializeField] private GameObject Friend;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;

        gameObject.SetActive(false);
    }
    public void SetUp(Vector3 pos)
    {
        gameObject.SetActive(true);
        gameObject.transform.position = pos + Vector3.up * height;
    }

    public void Clear()
    {
        gameObject.SetActive(false);
    }

    public void GetFrirendInfo(FrirendInfo frendinfo) // ��ȭ���� ������ ���� ��� => csv �����ɷ� ������ ������ �̸��� ���� �ٸ���
    {
        friendInfo = frendinfo;
    }
    public void SetHello()
    {
        // ������ ��� �Լ� ���⼭ ��  �÷��̾� transform ������ dir ����
        // �÷��̾ ������ ���� �ٶ�
        // ������ �λ� �ִϸ��̼� �ð� ������ �� �ð����� �÷��̾� ��������
    }
}
