using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 카드들 매니징해주는 컴포넌트
/// 여기서 카드 확률 높아지는 것 하나 정함
/// </summary>
public class CardsManager : MonoBehaviour
{
    public GameObject[] cardsArr = new GameObject[30]; // 총 인스턴시에이트 할 카드 갯수
    string[] cardName = new string[]{"Rock", "Scissors", "Paper"}; // 카드 종류 3개 프리팹 주소 뒤에 쓰일 것
    string root = "Prefab_VoteRSP/"; // 프리팹 주소

    Vector3 VoteEulerAngle =  new Vector3 ( 90, 0, 0); // 처음 오일러앵글

    public GameObject voteBox; // 시작 시 카드가 들어갈 voteBox

    public GameObject setCardPosition; // 밑의 부모 오브젝트

    public Transform[] CardPositionArr; // 테이블 위 카드 위치 담을 배열

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
    }
    void Start()
    {
        CardPositionArr = setCardPosition.GetComponentsInChildren<Transform>(); // 이때 0은 자기 자신이므로 1부터
        //Reset();
    }
    public void InstantiateCards()
    {
        //처음에 카드 생성 
        for(int i = 0; i < cardsArr.Length; i++)
        {
            // 인스턴시에이트 위치는 투표함 위
            int randomNum = UnityEngine.Random.Range(0, cardName.Length); // 0-2중 하나 랜덤으로 생성
            cardsArr[i] = Instantiate(Resources.Load(root + cardName[randomNum]) as GameObject, transform);
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

    public void Reset() => StartCoroutine(E_Reset());//InstantiateCards();

    IEnumerator E_Reset()
    {
        Transform[] child = GetComponentsInChildren<Transform>();

        if(child.Length > 0)
        {
            foreach(Transform t in child)
            {
                 if (t.gameObject == this.gameObject)
                    continue;
                 Destroy(t.gameObject);
            }
        }

        yield return null;

        InstantiateCards();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
