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

    public void GetFrirendInfo(FriendInfo frendinfo) // 대화에서 프렌드 정보 사용 => csv 같은걸로 프렌드 정보에 이름에 따라 다르게
    {
        friendInfo = frendinfo;
    }
    
    public void SetTalk(bool TorF)
    {
        isTalk = TorF; // 나중엔 대화끝나면 이즈텈 바귀게 할거임 
        instance.Clear();
        PlayerUI.instance.friendInfo = friendInfo; // talk클릭 시 플레이어UI에 정보 전달해서 대화 ㄱ
        PlayerUI.instance.SetTalk(TorF);
        TalkManager.instance.GetTalkData(friendInfo);
    }

}
