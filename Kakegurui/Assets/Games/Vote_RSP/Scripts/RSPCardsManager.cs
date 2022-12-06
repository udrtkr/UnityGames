using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 카드들 매니징해주는 컴포넌트
/// 여기서 카드 확률 낮아지는 것 하나 정함 -> ui 거울로 확인
/// </summary>
public class RSPCardsManager : MonoBehaviour
{
    public GameObject[] cardsArr = new GameObject[30]; // 총 카드 갯수

    string root = "Prefab_VoteRSP/"; // 프리팹 주소

    

    public GameObject voteBox; // 시작 시 카드가 들어갈 voteBox

    public GameObject setCardPosition; // 밑의 부모 오브젝트
    public Transform[] CardPositionArr; // 테이블 위 카드 위치 담을 배열

    public GameObject[] ChooseCards = new GameObject[3]; // 플레이어가 테이블에서 선택한 카드 세 개 담을 배열, null로 리셋
    public GameObject ChooseCardPosition; // 밑의 부모 오브젝트
    public Transform[] ChoosePositionArr; // 테이블에서 뽑은 카드 세 장 transform 담을 배열

    public GameObject[] ChooseCards_Opp = new GameObject[3]; // 상대방이 테이블에서 선택한 카드 세 개 담을 배열, null로 리셋
    public GameObject ChooseCardPosition_Opp; // 밑의 부모 오브젝트
    public Transform[] ChoosePositionArr_Opp; // 상대방의 테이블에서 뽑은 카드 세 장 transform 담을 배열

    public GameObject[] TurnOutCardArr_PlayerAndOpp = new GameObject[2]; // 플레이어와 상대방의 카드 타입 저장한 배열, 0=player 1=opp, null로 리셋
    

    private void Awake()
    {
        if (voteBox == null)
        {
            voteBox = GameObject.FindWithTag("VoteBox");
        }

        if(setCardPosition == null)
        {
            setCardPosition = GameObject.FindWithTag("SetCardPosition");
        }

        if(ChooseCardPosition == null)
        {
            ChooseCardPosition = GameObject.FindWithTag("ChoosePosition");
        }

        if(ChooseCardPosition_Opp == null)
        {
            ChooseCardPosition_Opp = GameObject.FindWithTag("ChoosePosition_Opp");
        }
    }
    void Start()
    {
        CardPositionArr = setCardPosition.GetComponentsInChildren<Transform>(); // 자신도 트랜스폼컴포넌트 ㅇ 이때 0은 자기 자신이므로 1부터
        ChoosePositionArr = ChooseCardPosition.GetComponentsInChildren<Transform>(); // 이때 0은 자기 자신이므로 1부터
        ChoosePositionArr_Opp = ChooseCardPosition_Opp.GetComponentsInChildren<Transform>(); // 이때 0은 자기 자신이므로 1부터
        //Reset();
    }

    //objectpool식처럼 자신 sprite만 바꾸게

    public void InstanciateCards() // Start에서 한번만 사용, 30장의 카드 프리팹 소환
    {
        if (transform.GetComponentsInChildren<RSPCard>().Length == 0)
        {
            //처음에 카드 생성 
            for (int i = 0; i < cardsArr.Length; i++)
            {
                // 인스턴시에이트 위치는 투표함 위
                //int randomNum = UnityEngine.Random.Range(0, cardName.Length); // 0-2중 하나 랜덤으로 생성
                cardsArr[i] = Instantiate(Resources.Load(root + "RSP_Card") as GameObject, transform);
            }
        }
    }
    public void ResetCards() // 게임 시작 시 카드 30장의 종류 정하는 메서드
    {
        //카드 종류 리셋
        for (int i = 0; i < cardsArr.Length; i++)
        {
            // 인스턴시에이트 위치는 투표함 위
            int randomNum = UnityEngine.Random.Range(0, 3); // 0-2중 하나 랜덤으로 생성
            cardsArr[i].GetComponent<RSPCard>().Reset(randomNum);// RSPCard에서 리셋(스프라이트 설정, 포지션 각도 리셋 등) 메서드 가져옴
            //cardsArr[i].transform.position = this.transform.position;

        }
    }
    // 매니저에서 뽑은 세 카드 저장, 각 배열에 저장해야댐
    public void VoteCards()
    {
        // 카드 투표박스 안으로 넣음 코루틴으로 순서대로 진행
        StartCoroutine(E_VoteCards());
    }
    IEnumerator E_VoteCards()
    {
        for(int i = 0; i < cardsArr.Length; i++)
        {
            cardsArr[i].GetComponent<RSPCard>().MoveToVoteBox(voteBox.transform.position);
            yield return new WaitForSeconds(0.15f);
        }

        yield return new WaitForSeconds(2);
        VoteRSP_Manager.Instance.SetOK = true; // 카드 voteBox에 모두 들어가면 SetOK = true, 카드 테이블에 세팅준비 ok
        yield return null;
    }
    public void SetCards()
    {
        // 카드 테이블에 셋팅
        // 각 카드에 컴포넌트 붙여야할 듯 움직이는 컴포넌트
        StartCoroutine (E_SetCards());
    }
    IEnumerator E_SetCards()
    {
        for(int i=0; i < CardPositionArr.Length-1; i++)
        {
            cardsArr[i].GetComponent<RSPCard>().MoveToTable(CardPositionArr[i+1].position);
            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(1);
        VoteRSP_Manager.Instance.SelectOK = true; // 모든 카드 모두 테이블에 세팅하면 카드 고를 준비 ok
        yield return null;
    }

    public void SetChooseCardAndPos(int num, GameObject card) // RSPCard 컴포넌트에서 사용, 자신의 카드가 골라지면 포지션 지정
    {
        ChooseCards[num] = card; // 플레이어가 고른 카드 담은 배열에 저장 하나씩
        card.transform.position = ChoosePositionArr[num+1].transform.position; // 카드 포지션 지정
    }

    public void SetChooseCardPos_Opp(int num, GameObject card)
    {
        card.transform.position = ChoosePositionArr_Opp[num+1].transform.position;
        card.transform.eulerAngles = new Vector3(-90, 0, 90);
    }

    public void OppCardsSetRemain() // 상대방 카드 세개 골라야함
    {
        // 카드들 중 선택 안된 애들만 찾아서 list에 넣은 후 랜덤으로 세 개 선택해서 매니저에 전달, 매니저에서 사용해야함
        List<RSPCard> cards = new List<RSPCard>(GetComponentsInChildren<RSPCard>()); // RSPCard 컴포넌트 붙은 자식만 데려옴

        List<RSPCard> RemainCards = cards.FindAll(t => t.isChoosed == false); // 플레이어가 고른 카드 외의 카드만 가져옴

        for (int i = 0; i < 3; i++) // 남은 카드들 중 랜덤으로 세 개 선택하여 배열에 저장
        {
            int random = UnityEngine.Random.Range(0, RemainCards.Count);
            ChooseCards_Opp[i] = RemainCards[random].gameObject;
            RemainCards.RemoveAt(random);
        }

        // 카드 위치 옮긴 후 낼 카드 선택
        for (int i = 0; i < ChooseCards_Opp.Length; i++) 
        {
            SetChooseCardPos_Opp(i, ChooseCards_Opp[i]); // 여기가 문제 Index was outside the bounds of the array
        }

        int ChooseOne = UnityEngine.Random.Range(0, ChooseCards_Opp.Length); // 세 개 중 하나 랜덤으로 고름
        SetPlayerOppTypeArr(1, ChooseCards_Opp[ChooseOne]); // 카드 타입비교 위해 카드 오브젝트 저장
        //PlayerOppCardTypeArr[1] = ChooseCards_Opp[ChooseOne].GetComponent<RSPCard>().rspType; 
        // 고른 카드 enum타입 배열에 전달
    }

    public void SetPlayerOppTypeArr(int who, GameObject rspTypeObj) // 비교할 타입 배열에 플레이어 카드타입, 상대방 카드타입 세팅할 메서드
    {
        TurnOutCardArr_PlayerAndOpp[who] = rspTypeObj;
    }

    public int GetCompareCardsPlayerAndOpp() // 경우의 수에 따라 결과 달라짐, 반환 0 = draw 1 = Win -1 = Lose
    {
        int result = 0;
        switch ((int)TurnOutCardArr_PlayerAndOpp[0].GetComponent<RSPCard>().rspType - (int)TurnOutCardArr_PlayerAndOpp[1].GetComponent<RSPCard>().rspType) // 플레이어 타입 enum - 상대방 타입 enum
        {
            // 묵=0 찌=1 빠=2
            case -1:
                result = 1; // 묵 찌, 찌 빠
                break;
            case 1:
                result = -1; // 찌 묵, 빠 찌
                break;
            case -2:
                result = -1; // 묵 빠
                break;
            case 2:
                result = 1; // 빠 묵
                break;
            default:
                break; // 비길 시 값 바꾸지 x 0
        }
        return result;
    }

    public void TurnOutCard() // 카드 패 천천히 공개하는 메서드
    {
        StartCoroutine(E_TurnOutCard());
    }

    IEnumerator E_TurnOutCard() // 카드 패 천천히 공개하는 코루틴
    {
        Debug.Log(ChooseCards[0].transform.eulerAngles);
        float speed = 0.002f;
        while(ChooseCards[0].transform.eulerAngles.x > 1) // 카드 비교 전, 먼저 둘 다 카드 세개 패 모두 공개
        {
            for(int i = 0; i < ChooseCards.Length; i++)
            {
                ChooseCards[i].transform.eulerAngles += new Vector3(1,0,0)*speed*75; // 천천히 세 카드 공개
                ChooseCards_Opp[i].transform.eulerAngles += new Vector3(1,0,0)*speed*75;
            }
            yield return new WaitForSeconds(0.005f);
        }
        yield return new WaitForSeconds(0.5f);

        float originX = TurnOutCardArr_PlayerAndOpp[0].transform.position.x; // 플레이어 turnout card의 기존 x 위치

        while(originX - TurnOutCardArr_PlayerAndOpp[0].transform.position.x <= 0.22) // 카드가 움직일 거리
        {
            TurnOutCardArr_PlayerAndOpp[0].transform.position -= new Vector3(1,0,0)*speed;
            TurnOutCardArr_PlayerAndOpp[1].transform.position += new Vector3(1,0,0)*speed;
            yield return new WaitForSeconds(0.015f);
        }
        yield return null;

        VoteRSP_Manager.Instance.ShowResultOK = true;
    }

    public void Reset() 
    {
        for(int i=0; i< ChooseCards_Opp.Length; i++)
        {
            ChooseCards_Opp[i] = null;
            ChooseCards_Opp[i] = null;
        }
        TurnOutCardArr_PlayerAndOpp[0] = null;
        TurnOutCardArr_PlayerAndOpp[1] = null;

        InstanciateCards();
        ResetCards();
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
