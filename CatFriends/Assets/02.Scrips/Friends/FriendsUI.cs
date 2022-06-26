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

    public void GetFrirendInfo(FrirendInfo frendinfo) // 대화에서 프렌드 정보 사용 => csv 같은걸로 프렌드 정보에 이름에 따라 다르게
    {
        friendInfo = frendinfo;
    }
    public void SetHello()
    {
        // 프렌드 헬로 함수 여기서 씀  플레이어 transform 가저와 dir 적용
        // 플레이어도 프렌드 방향 바라봄
        // 프렌드 인사 애니메이션 시간 가져와 그 시간동안 플레이어 정지시켜
    }
}
