using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 각 문의 특징들 담을 스크립트
// 콜라이더 컴포넌트 가져와서 처리
public class Doorway : MonoBehaviour
{
    public string gameName;
    public string sceneName;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // UI_Door 문앞으로 이동시킴
            UI_Door.Instance.SetPosition(transform.position);
            // UI_Door 에서 메인매니저의 씬변경 메서드 가져와 클릭으로 만듬, UI_Door에 정보 전달 후 클릭시 씬 변경
            UI_Door.Instance.SetGameNameAndSceneName(gameName, sceneName);
            // 정보 전달한 것 기반으로 UI에 뜨는 게임 이름, 씬 이름 변경, 게임 이름은 나타나게
        }
        Debug.Log("등장");
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            UI_Door.Instance.SetPosition(new Vector3(999,999,999)); // 문 영역 밖으로 나가면 안보이게 멀리 날려버림
            UI_Door.Instance.SetGameNameAndSceneName("", ""); // 초기화
        }
    }
}
