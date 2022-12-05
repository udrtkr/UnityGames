using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RSPCard : MonoBehaviour
{
    public RSPtype rspType;

    public Sprite[] RSPsprites = new Sprite[3]; // ���� ���� �� ���� �ϳ��� ��������Ʈ ���� �迭, ���� ������ ����

    //string[] cardName = new string[] { "Rock", "Scissors", "Paper" }; // ī�� ���� 3�� ������ �ּ� �ڿ� ���� ��
    string root = "Prefab_VoteRSP/"; // ������ �ּ�

    private float moveSpeed = 0.025f;
    private Vector3 eulerTableCard = new Vector3 (180,-90,0);
    private Vector3 eulerChoosedCard = new Vector3(-90, -90, 0);
    private Vector3 eulerShowCard = new Vector3 (0,-90,0);
    private bool isSelected = false; // ���̺� ������ ���콺 �÷��� ���� ��

    public bool isChoosed = false; // �� �� �� ���õ� ī��

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
            // enum , resouce �̿��Ͽ� ��������Ʈ �迭�� ����
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
                VoteRSP_Manager.Instance.SelectCardNum++; // �� ī�� ��
                transform.parent.gameObject.GetComponent<RSPCardsManager>().SetChooseCardPos(VoteRSP_Manager.Instance.SelectCardNum, gameObject);
                transform.eulerAngles = eulerChoosedCard;

                Debug.Log("selected");
            }
        }
        
        if(VoteRSP_Manager.Instance.turnOutOK) // ī�� ���� �� �� �����ϰ�
        {
            // ���� �� ����, ���� �ణ �ö󰡰�
        }
    }

    public void SetRSP(int spriteNum)
    {
        
        RSP.GetComponent<SpriteRenderer>().sprite = RSPsprites[spriteNum];
    }

    public void MoveToVoteBox(Vector3 voteBox) // voteBox�� ī�� �̵��ϴ� �޼���
    {
        Vector3 dir = (voteBox - transform.position).normalized;
        StartCoroutine(E_MoveTo(dir, voteBox, 0.03f));
    }

    public void MoveToTable(Vector3 tablenum) // voteBox���� ���̺�� ī�� �����ϴ� �޼���
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
        if (VoteRSP_Manager.Instance.SelectOK && !isChoosed) // ī�� ���� ������ ��, ���õ� ī�尡 �ƴ� ��
        {
            // ī�������Ʈ Ƽ���� && �Ŵ����� ī�� ���� num++
            // ���� ���� num > 2 �̸� SelectOK = false
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
        isSelected = false; // ���̺� ������ ���콺 �÷��� ���� ��

        isChoosed = false; // �� �� �� ���õ� ī��

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
