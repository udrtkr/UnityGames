using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 카드들 매니징해주는 컴포넌트
/// 여기서 카드 확률 높아지는 것 하나 정함 -> ui 거울로 확인
/// </summary>
public class RSPCardsManager : MonoBehaviour
{
    public GameObject[] cardsArr = new GameObject[30]; // 총 카드 갯수

    string root = "Prefab_VoteRSP/"; // 프리팹 주소

    Vector3 VoteEulerAngle =  new Vector3 ( 90, 0, 0); // 처음 오일러앵글

    public GameObject voteBox; // 시작 시 카드가 들어갈 voteBox

    public GameObject setCardPosition; // 밑의 부모 오브젝트
    public Transform[] CardPositionArr; // 테이블 위 카드 위치 담을 배열

    public GameObject ChooseCardPosition; // 밑의 부모 오브젝트
    public Transform[] ChoosePositionArr; // 테이블에서 뽑은 카드 세 장 transform 담을 배열

    public GameObject[] ChooseCards_Opp = new GameObject[3];
    public GameObject ChooseCardPosition_Opp; // 밑의 부모 오브젝트
    public Transform[] ChoosePositionArr_Opp; // 상대방의 테이블에서 뽑은 카드 세 장 transform 담을 배열
    

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
        ChoosePositionArr_Opp = ChooseCardPosition_Opp.GetComponentsInChildren<Transform>();
        //Reset();
    }

    //objectpool로 바꾸어 자신 sprite만 바꾸게 해보기 
    public void InstanciateCards() // 한번만 사용
    {
        if (transform.GetComponentsInChildren<RSPCard>().Length == 0)
        {
            //처음에 카드 생성 
            for (int i = 0; i < cardsArr.Length; i++)
            {
                // 인스턴시에이트 위치는 투표함 위
                //int randomNum = UnityEngine.Random.Range(0, cardName.Length); // 0-2중 하나 랜덤으로 생성
                cardsArr[i] = Instantiate(Resources.Load(root + "RSP_Card") as GameObject, transform);
                cardsArr[i].transform.eulerAngles = VoteEulerAngle;
            }
        }
    }
    public void ResetCards()
    {
        //카드 종류 리셋
        for (int i = 0; i < cardsArr.Length; i++)
        {
            // 인스턴시에이트 위치는 투표함 위
            int randomNum = UnityEngine.Random.Range(0, 3); // 0-2중 하나 랜덤으로 생성
            cardsArr[i].GetComponent<RSPCard>().SetRSP(randomNum); // RSPCard에서 자식 스프라이트 설정 메서드 가져옴
            cardsArr[i].transform.eulerAngles = VoteEulerAngle;
        }
    }
    // 매니저에서 뽑은 세 카드 저장, 각 배열에 저장해야댐
    public void VoteCards()
    {
        // 카드 투표박스 안으로 넣음 코루틴으로 순서대로 진행
        // 각 카드에 컴포넌트 붙여야할 듯 움직이는 컴포넌트
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

    public void SetChooseCardPos(int num, GameObject card)
    {
        card.transform.position = ChoosePositionArr[num].transform.position;
    }

    public void SetChooseCardPos_Opp(int num, GameObject card)
    {
        card.transform.position = ChoosePositionArr_Opp[num+1].transform.position; // 이거 문제 x
        card.transform.eulerAngles = new Vector3(-90, 0, 90);
    }

    public void OppCardsSetRemain() // 상대방 카드 세개 골라야함
    {
        // 카드들 중 선택 안된 애들만 찾아서 list에 넣은 후 랜덤으로 세 개 선택해서 매니저에 전달, 매니저에서 사용해야함
        List<RSPCard> cards = new List<RSPCard>(GetComponentsInChildren<RSPCard>()); // 카드 컴포넌트 붙은 자식만 데려옴

        List<RSPCard> RemainCards = cards.FindAll(t => t.isChoosed == false); //

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

        int randomChice = UnityEngine.Random.Range(0, ChooseCards_Opp.Length);
        // 고른 카드 배열 매니저에 전달
    }

    public void Reset() 
    {
        for(int i=0; i< ChooseCards_Opp.Length; i++)
        {
            ChooseCards_Opp[i] = null;
        }
        InstanciateCards();
        ResetCards();
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
