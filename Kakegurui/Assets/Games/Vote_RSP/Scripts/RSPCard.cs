using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RSPCard : MonoBehaviour
{
    public RSPtype rspType;

    public Sprite[] RSPsprites = new Sprite[3]; // 가위 바위 보 각각 하나씩 스프라이트 담은 배열, 랜덤 결정에 쓰임

    //string[] cardName = new string[] { "Rock", "Scissors", "Paper" }; // 카드 종류 3개 프리팹 주소 뒤에 쓰일 것
    string root = "Prefab_VoteRSP/"; // 프리팹 주소

    private float moveSpeed = 0.025f;
    private Vector3 VoteEulerAngle = new Vector3(90, 0, 0); // 처음 오일러앵글
    private Vector3 eulerTableCard = new Vector3 (180,-90,0);
    private Vector3 eulerChoosedCard = new Vector3(-90, -90, 0);
    private Vector3 eulerShowCard = new Vector3 (0,-90,0);
    private bool isSelected = false; // 테이블 위 or 세 장 카드 낼 때 상황에서 자신의 오브젝트(카드)에 마우스 올려져 있을 때

    public bool isChoosed = false; // 세 장 중 선택된 카드

    private SpriteRenderer RSPspriteRenderer;

    private void Awake()
    {
        RSPspriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        
        SetRSPsprite();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void SetRSPsprite() //  처음에 가위 바위 보 스프라이트 배열에 스프라이트들 저장해주는 메서드
    {
        for(int i = 0; i < RSPsprites.Length; i++)
        {
            // enum , resouce 이용하여 스프라이트 배열에 넣음
            RSPsprites[i] = Resources.Load<Sprite>(root + Enum.GetName(typeof(RSPtype),i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (VoteRSP_Manager.Instance.SelectOK && isSelected && !isChoosed) // 매니저에서 테이블에서 골라도 되는 타이밍 ㅇ, 선택된 카드가 아니고, 올려져 있을 때
            {
                isChoosed = true;
                transform.parent.gameObject.GetComponent<RSPCardsManager>().SetChooseCardAndPos(VoteRSP_Manager.Instance.SelectCardNum, this.gameObject); // 고른 카드 위치, 자신 오브젝트 선택된 배열에 저장
                VoteRSP_Manager.Instance.SelectCardNum++; // 고른 카드 수
                transform.eulerAngles = eulerChoosedCard;

                Debug.Log("selected");

            }

            if (VoteRSP_Manager.Instance.turnOutOK && isChoosed && isSelected) //  세 장 중 선택된 카드이고, 마우스 올려져 isSelected = true 일 때 
            {
                VoteRSP_Manager.Instance.turnOutOK = false; // 세 장 중 고르는 상황 지났으므로 false
                VoteRSP_Manager.Instance.isTurnOut = true; // 세장 중 선택 했으므로 카드 내도 됨
                transform.parent.gameObject.GetComponent<RSPCardsManager>().SetPlayerOppTypeArr(0, this.gameObject); // 부모 컴포넌트의 상대방과 타입 비교위해 선택된 카드 arr에 저장
            }
        }
    }

    public void SetRSP(int spriteNum) // 리셋 시 처음에 셋팅할 것들 담은 메서드
    {
        isSelected = false;
        isChoosed = false;
        RSPspriteRenderer.sprite = RSPsprites[spriteNum]; // 바위 가위 보 중 스프라이트 변경해줌
        rspType = (RSPtype)spriteNum; // 가위 바위 보 중 enum 타입으로 저장
    }

    public void MoveToVoteBox(Vector3 voteBox) // voteBox로 카드 이동하는 메서드
    {
        Vector3 dir = (voteBox - transform.position).normalized;
        StartCoroutine(E_MoveTo(dir, voteBox, 0.03f));
    }

    public void MoveToTable(Vector3 tablenum) // voteBox에서 테이블로 카드 세팅하는 메서드
    {
        transform.eulerAngles = eulerTableCard;
        Vector3 dir = (tablenum - transform.position).normalized;
        StartCoroutine(E_MoveTo(dir, tablenum, 0.05f));
    }
    IEnumerator E_MoveTo(Vector3 direction , Vector3 tablenum, float errorScope)
    {
        while((transform.position - tablenum).magnitude > errorScope)
        {
            transform.position += direction * moveSpeed;
            yield return new WaitForSeconds(0.005f);
        }

        yield return null;
    }


    private void OnMouseEnter()
    {
        if (VoteRSP_Manager.Instance.SelectOK && !isChoosed) // 카드 선택 가능할 시, 선택된 카드가 아닐 때
        {
            // 카드오브젝트 티나게 && 매니저에 카드 선택 num++
            // 만약 선택 num > 2 이면 SelectOK = false
            isSelected = true;
            transform.position += new Vector3(0, 0.02f, 0); // 선택 시 위로 살짝 들려짐
        }

        if(isChoosed && VoteRSP_Manager.Instance.turnOutOK) // 3개 중 선택된 카드이고, 카드 내는 타이밍이 ok 일 때
        {
            // 세장 중 선택, 위로 약간 올라가게
            isSelected = true;
            transform.position += new Vector3(0, 0.01f, 0); // 선택 시 위로 살짝 들려짐
        }
    }
    private void OnMouseExit()
    {
        if (isSelected && !isChoosed) // 선택된 3카드가 아니고, 마우스 올려져서 isSelect = true 일 때
        {
            isSelected = false;
            transform.position -= new Vector3(0, 0.02f, 0);
        }

        if (isChoosed && isSelected) // 선택된 3카드 중 하나이고, 마우스 올려져서 isSelect = true일 때
        {
            isSelected = false;
            transform.position -= new Vector3(0, 0.01f, 0);
        }
    }

    public void Reset(int spriteNum)
    {
        isSelected = false; // 테이블 위에서 마우스 올려져 있을 때

        isChoosed = false; // 세 장 중 선택된 카드 t or f false로

        transform.Translate(Vector3.zero);
        transform.eulerAngles = VoteEulerAngle;

        SetRSP(spriteNum);
    }
}

public enum RSPtype // 바위 가위 보를 순서대로 담은 enum, 숫자나 문자 형태로 쓰일거임
{
    Rock,
    Scissors,
    Papper
}
