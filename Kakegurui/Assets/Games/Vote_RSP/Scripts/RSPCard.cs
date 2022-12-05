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
    private Vector3 eulerTableCard = new Vector3 (180,-90,0);
    private Vector3 eulerChoosedCard = new Vector3(-90, -90, 0);
    private Vector3 eulerShowCard = new Vector3 (0,-90,0);
    private bool isSelected = false; // 테이블 위에서 마우스 올려져 있을 때

    public bool isChoosed = false; // 세 장 중 선택된 카드

    private bool isTurnOut = false;

    private GameObject RSP;

    private void Awake()
    {
        RSP = transform.Find("RSP").gameObject;
        
        SetRSPsprite();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void SetRSPsprite()
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
        if (isSelected && !isChoosed)
        {
            if (Input.GetMouseButtonUp(0))
            {
                isChoosed = true;
                VoteRSP_Manager.Instance.SelectCardNum++; // 고른 카드 수
                transform.parent.gameObject.GetComponent<RSPCardsManager>().SetChooseCardPos(VoteRSP_Manager.Instance.SelectCardNum, gameObject);
                transform.eulerAngles = eulerChoosedCard;

                Debug.Log("selected");
            }
        }
        
        if(VoteRSP_Manager.Instance.turnOutOK) // 카드 내도 될 때 선택하게
        {
            // 세장 중 선택, 위로 약간 올라가게
        }
    }

    public void SetRSP(int spriteNum)
    {
        
        RSP.GetComponent<SpriteRenderer>().sprite = RSPsprites[spriteNum];
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
            yield return new WaitForSeconds(0.01f);
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
            transform.position += new Vector3(0, 0.02f, 0);
        }

        if(isChoosed && VoteRSP_Manager.Instance.turnOutOK)
        {
            isTurnOut = true;
        }
    }
    private void OnMouseExit()
    {
        if (isSelected && !isChoosed) 
        {
            isSelected = false;
            transform.position -= new Vector3(0, 0.02f, 0);
        }

        if (isChoosed && isTurnOut)
        {
            isTurnOut=false;
        }
    }

    public void Reset()
    {
        isSelected = false; // 테이블 위에서 마우스 올려져 있을 때

        isChoosed = false; // 세 장 중 선택된 카드

        isTurnOut = false;

        transform.Translate(Vector3.zero);
    }
}

public enum RSPtype
{
    Rock,
    Scissors,
    Papper
}
