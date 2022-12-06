using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ī��� �Ŵ�¡���ִ� ������Ʈ
/// ���⼭ ī�� Ȯ�� �������� �� �ϳ� ���� -> ui �ſ�� Ȯ��
/// </summary>
public class RSPCardsManager : MonoBehaviour
{
    public GameObject[] cardsArr = new GameObject[30]; // �� ī�� ����

    string root = "Prefab_VoteRSP/"; // ������ �ּ�

    

    public GameObject voteBox; // ���� �� ī�尡 �� voteBox

    public GameObject setCardPosition; // ���� �θ� ������Ʈ
    public Transform[] CardPositionArr; // ���̺� �� ī�� ��ġ ���� �迭

    public GameObject[] ChooseCards = new GameObject[3]; // �÷��̾ ���̺��� ������ ī�� �� �� ���� �迭, null�� ����
    public GameObject ChooseCardPosition; // ���� �θ� ������Ʈ
    public Transform[] ChoosePositionArr; // ���̺��� ���� ī�� �� �� transform ���� �迭

    public GameObject[] ChooseCards_Opp = new GameObject[3]; // ������ ���̺��� ������ ī�� �� �� ���� �迭, null�� ����
    public GameObject ChooseCardPosition_Opp; // ���� �θ� ������Ʈ
    public Transform[] ChoosePositionArr_Opp; // ������ ���̺��� ���� ī�� �� �� transform ���� �迭

    public GameObject[] TurnOutCardArr_PlayerAndOpp = new GameObject[2]; // �÷��̾�� ������ ī�� Ÿ�� ������ �迭, 0=player 1=opp, null�� ����
    

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
        CardPositionArr = setCardPosition.GetComponentsInChildren<Transform>(); // �ڽŵ� Ʈ������������Ʈ �� �̶� 0�� �ڱ� �ڽ��̹Ƿ� 1����
        ChoosePositionArr = ChooseCardPosition.GetComponentsInChildren<Transform>(); // �̶� 0�� �ڱ� �ڽ��̹Ƿ� 1����
        ChoosePositionArr_Opp = ChooseCardPosition_Opp.GetComponentsInChildren<Transform>(); // �̶� 0�� �ڱ� �ڽ��̹Ƿ� 1����
        //Reset();
    }

    //objectpool��ó�� �ڽ� sprite�� �ٲٰ�

    public void InstanciateCards() // Start���� �ѹ��� ���, 30���� ī�� ������ ��ȯ
    {
        if (transform.GetComponentsInChildren<RSPCard>().Length == 0)
        {
            //ó���� ī�� ���� 
            for (int i = 0; i < cardsArr.Length; i++)
            {
                // �ν��Ͻÿ���Ʈ ��ġ�� ��ǥ�� ��
                //int randomNum = UnityEngine.Random.Range(0, cardName.Length); // 0-2�� �ϳ� �������� ����
                cardsArr[i] = Instantiate(Resources.Load(root + "RSP_Card") as GameObject, transform);
            }
        }
    }
    public void ResetCards() // ���� ���� �� ī�� 30���� ���� ���ϴ� �޼���
    {
        //ī�� ���� ����
        for (int i = 0; i < cardsArr.Length; i++)
        {
            // �ν��Ͻÿ���Ʈ ��ġ�� ��ǥ�� ��
            int randomNum = UnityEngine.Random.Range(0, 3); // 0-2�� �ϳ� �������� ����
            cardsArr[i].GetComponent<RSPCard>().Reset(randomNum);// RSPCard���� ����(��������Ʈ ����, ������ ���� ���� ��) �޼��� ������
            //cardsArr[i].transform.position = this.transform.position;

        }
    }
    // �Ŵ������� ���� �� ī�� ����, �� �迭�� �����ؾߴ�
    public void VoteCards()
    {
        // ī�� ��ǥ�ڽ� ������ ���� �ڷ�ƾ���� ������� ����
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
        VoteRSP_Manager.Instance.SetOK = true; // ī�� voteBox�� ��� ���� SetOK = true, ī�� ���̺� �����غ� ok
        yield return null;
    }
    public void SetCards()
    {
        // ī�� ���̺� ����
        // �� ī�忡 ������Ʈ �ٿ����� �� �����̴� ������Ʈ
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
        VoteRSP_Manager.Instance.SelectOK = true; // ��� ī�� ��� ���̺� �����ϸ� ī�� �� �غ� ok
        yield return null;
    }

    public void SetChooseCardAndPos(int num, GameObject card) // RSPCard ������Ʈ���� ���, �ڽ��� ī�尡 ������� ������ ����
    {
        ChooseCards[num] = card; // �÷��̾ �� ī�� ���� �迭�� ���� �ϳ���
        card.transform.position = ChoosePositionArr[num+1].transform.position; // ī�� ������ ����
    }

    public void SetChooseCardPos_Opp(int num, GameObject card)
    {
        card.transform.position = ChoosePositionArr_Opp[num+1].transform.position;
        card.transform.eulerAngles = new Vector3(-90, 0, 90);
    }

    public void OppCardsSetRemain() // ���� ī�� ���� ������
    {
        // ī��� �� ���� �ȵ� �ֵ鸸 ã�Ƽ� list�� ���� �� �������� �� �� �����ؼ� �Ŵ����� ����, �Ŵ������� ����ؾ���
        List<RSPCard> cards = new List<RSPCard>(GetComponentsInChildren<RSPCard>()); // RSPCard ������Ʈ ���� �ڽĸ� ������

        List<RSPCard> RemainCards = cards.FindAll(t => t.isChoosed == false); // �÷��̾ �� ī�� ���� ī�常 ������

        for (int i = 0; i < 3; i++) // ���� ī��� �� �������� �� �� �����Ͽ� �迭�� ����
        {
            int random = UnityEngine.Random.Range(0, RemainCards.Count);
            ChooseCards_Opp[i] = RemainCards[random].gameObject;
            RemainCards.RemoveAt(random);
        }

        // ī�� ��ġ �ű� �� �� ī�� ����
        for (int i = 0; i < ChooseCards_Opp.Length; i++) 
        {
            SetChooseCardPos_Opp(i, ChooseCards_Opp[i]); // ���Ⱑ ���� Index was outside the bounds of the array
        }

        int ChooseOne = UnityEngine.Random.Range(0, ChooseCards_Opp.Length); // �� �� �� �ϳ� �������� ��
        SetPlayerOppTypeArr(1, ChooseCards_Opp[ChooseOne]); // ī�� Ÿ�Ժ� ���� ī�� ������Ʈ ����
        //PlayerOppCardTypeArr[1] = ChooseCards_Opp[ChooseOne].GetComponent<RSPCard>().rspType; 
        // �� ī�� enumŸ�� �迭�� ����
    }

    public void SetPlayerOppTypeArr(int who, GameObject rspTypeObj) // ���� Ÿ�� �迭�� �÷��̾� ī��Ÿ��, ���� ī��Ÿ�� ������ �޼���
    {
        TurnOutCardArr_PlayerAndOpp[who] = rspTypeObj;
    }

    public int GetCompareCardsPlayerAndOpp() // ����� ���� ���� ��� �޶���, ��ȯ 0 = draw 1 = Win -1 = Lose
    {
        int result = 0;
        switch ((int)TurnOutCardArr_PlayerAndOpp[0].GetComponent<RSPCard>().rspType - (int)TurnOutCardArr_PlayerAndOpp[1].GetComponent<RSPCard>().rspType) // �÷��̾� Ÿ�� enum - ���� Ÿ�� enum
        {
            // ��=0 ��=1 ��=2
            case -1:
                result = 1; // �� ��, �� ��
                break;
            case 1:
                result = -1; // �� ��, �� ��
                break;
            case -2:
                result = -1; // �� ��
                break;
            case 2:
                result = 1; // �� ��
                break;
            default:
                break; // ��� �� �� �ٲ��� x 0
        }
        return result;
    }

    public void TurnOutCard() // ī�� �� õõ�� �����ϴ� �޼���
    {
        StartCoroutine(E_TurnOutCard());
    }

    IEnumerator E_TurnOutCard() // ī�� �� õõ�� �����ϴ� �ڷ�ƾ
    {
        Debug.Log(ChooseCards[0].transform.eulerAngles);
        float speed = 0.002f;
        while(ChooseCards[0].transform.eulerAngles.x > 1) // ī�� �� ��, ���� �� �� ī�� ���� �� ��� ����
        {
            for(int i = 0; i < ChooseCards.Length; i++)
            {
                ChooseCards[i].transform.eulerAngles += new Vector3(1,0,0)*speed*75; // õõ�� �� ī�� ����
                ChooseCards_Opp[i].transform.eulerAngles += new Vector3(1,0,0)*speed*75;
            }
            yield return new WaitForSeconds(0.005f);
        }
        yield return new WaitForSeconds(0.5f);

        float originX = TurnOutCardArr_PlayerAndOpp[0].transform.position.x; // �÷��̾� turnout card�� ���� x ��ġ

        while(originX - TurnOutCardArr_PlayerAndOpp[0].transform.position.x <= 0.22) // ī�尡 ������ �Ÿ�
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
