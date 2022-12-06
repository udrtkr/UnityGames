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
    private Vector3 VoteEulerAngle = new Vector3(90, 0, 0); // ó�� ���Ϸ��ޱ�
    private Vector3 eulerTableCard = new Vector3 (180,-90,0);
    private Vector3 eulerChoosedCard = new Vector3(-90, -90, 0);
    private Vector3 eulerShowCard = new Vector3 (0,-90,0);
    private bool isSelected = false; // ���̺� �� or �� �� ī�� �� �� ��Ȳ���� �ڽ��� ������Ʈ(ī��)�� ���콺 �÷��� ���� ��

    public bool isChoosed = false; // �� �� �� ���õ� ī��

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

    void SetRSPsprite() //  ó���� ���� ���� �� ��������Ʈ �迭�� ��������Ʈ�� �������ִ� �޼���
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
        if (Input.GetMouseButtonUp(0))
        {
            if (VoteRSP_Manager.Instance.SelectOK && isSelected && !isChoosed) // �Ŵ������� ���̺��� ��� �Ǵ� Ÿ�̹� ��, ���õ� ī�尡 �ƴϰ�, �÷��� ���� ��
            {
                isChoosed = true;
                transform.parent.gameObject.GetComponent<RSPCardsManager>().SetChooseCardAndPos(VoteRSP_Manager.Instance.SelectCardNum, this.gameObject); // �� ī�� ��ġ, �ڽ� ������Ʈ ���õ� �迭�� ����
                VoteRSP_Manager.Instance.SelectCardNum++; // �� ī�� ��
                transform.eulerAngles = eulerChoosedCard;

                Debug.Log("selected");

            }

            if (VoteRSP_Manager.Instance.turnOutOK && isChoosed && isSelected) //  �� �� �� ���õ� ī���̰�, ���콺 �÷��� isSelected = true �� �� 
            {
                VoteRSP_Manager.Instance.turnOutOK = false; // �� �� �� ���� ��Ȳ �������Ƿ� false
                VoteRSP_Manager.Instance.isTurnOut = true; // ���� �� ���� �����Ƿ� ī�� ���� ��
                transform.parent.gameObject.GetComponent<RSPCardsManager>().SetPlayerOppTypeArr(0, this.gameObject); // �θ� ������Ʈ�� ����� Ÿ�� ������ ���õ� ī�� arr�� ����
            }
        }
    }

    public void SetRSP(int spriteNum) // ���� �� ó���� ������ �͵� ���� �޼���
    {
        isSelected = false;
        isChoosed = false;
        RSPspriteRenderer.sprite = RSPsprites[spriteNum]; // ���� ���� �� �� ��������Ʈ ��������
        rspType = (RSPtype)spriteNum; // ���� ���� �� �� enum Ÿ������ ����
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
            yield return new WaitForSeconds(0.005f);
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
            transform.position += new Vector3(0, 0.02f, 0); // ���� �� ���� ��¦ �����
        }

        if(isChoosed && VoteRSP_Manager.Instance.turnOutOK) // 3�� �� ���õ� ī���̰�, ī�� ���� Ÿ�̹��� ok �� ��
        {
            // ���� �� ����, ���� �ణ �ö󰡰�
            isSelected = true;
            transform.position += new Vector3(0, 0.01f, 0); // ���� �� ���� ��¦ �����
        }
    }
    private void OnMouseExit()
    {
        if (isSelected && !isChoosed) // ���õ� 3ī�尡 �ƴϰ�, ���콺 �÷����� isSelect = true �� ��
        {
            isSelected = false;
            transform.position -= new Vector3(0, 0.02f, 0);
        }

        if (isChoosed && isSelected) // ���õ� 3ī�� �� �ϳ��̰�, ���콺 �÷����� isSelect = true�� ��
        {
            isSelected = false;
            transform.position -= new Vector3(0, 0.01f, 0);
        }
    }

    public void Reset(int spriteNum)
    {
        isSelected = false; // ���̺� ������ ���콺 �÷��� ���� ��

        isChoosed = false; // �� �� �� ���õ� ī�� t or f false��

        transform.Translate(Vector3.zero);
        transform.eulerAngles = VoteEulerAngle;

        SetRSP(spriteNum);
    }
}

public enum RSPtype // ���� ���� ���� ������� ���� enum, ���ڳ� ���� ���·� ���ϰ���
{
    Rock,
    Scissors,
    Papper
}
