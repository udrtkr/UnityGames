using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendsUI : MonoBehaviour
{
    public static FriendsUI instance;

    public FriendInfo friendInfo;
    public bool isTalk;

    [SerializeField] private float height = 1f;
    [SerializeField] private GameObject Friend;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;

        gameObject.SetActive(false);
    }
    public void SetUp(Vector3 pos, FriendInfo frendinfo)
    {
        gameObject.SetActive(true);
        gameObject.transform.position = pos + Vector3.up * height;
        friendInfo = frendinfo;
    }

    public void Clear()
    {
        gameObject.SetActive(false);
    }

    public void GetFrirendInfo(FriendInfo frendinfo) // ��ȭ���� ������ ���� ��� => csv �����ɷ� ������ ������ �̸��� ���� �ٸ���
    {
        friendInfo = frendinfo;
    }
    
    public void SetTalk(bool TorF)
    {
        isTalk = TorF; // ���߿� ��ȭ������ ��� �ٱͰ� �Ұ��� 
        instance.Clear();
        PlayerUI.instance.friendInfo = friendInfo; // talkŬ�� �� �÷��̾�UI�� ���� �����ؼ� ��ȭ ��
        PlayerUI.instance.SetTalk(TorF);
        TalkManager.instance.GetTalkData(friendInfo);
    }

}
