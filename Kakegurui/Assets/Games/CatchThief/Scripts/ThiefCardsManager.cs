using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefCardsManager : MonoBehaviour
{
    public GameObject[] ThiefCardsArr; // 자식 오브젝트 카드들 A, Joker, A 
    Vector3[] ResetCardsPos = { new Vector3(-0.2f, 0, 0), Vector3.zero, new Vector3(0.2f, 0, 0) };

    private void Awake()
    {
        if(ThiefCardsArr == null)
        {
            ThiefCardsArr = new GameObject[3];
            for(int i = 0; i < ThiefCardsArr.Length; i++)
            {
                ThiefCardsArr[i] = transform.GetChild(i).gameObject; // 첫번째 자식이 0번째
            }
        }
    }
    public void Reset()
    {
        for(int i = 0; i < ThiefCardsArr.Length; i++)
        {
            ThiefCardsArr[i].transform.localPosition = ResetCardsPos[i];
            ThiefCardsArr[i].GetComponent<ThiefCard>().Reset();
        }
    }

    // 게임 시작, 플레이어의 선조커 선택 후 카드 위치, 카드의 선점 세팅하는 메서드
    public void SetCardsStart(Transform[] firstJoker, Transform notJoker)
    {

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
