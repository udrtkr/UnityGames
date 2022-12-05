using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ī��� �Ŵ�¡���ִ� ������Ʈ
/// ���⼭ ī�� Ȯ�� �������� �� �ϳ� ���� -> ui �ſ�� Ȯ��
/// </summary>
public class RSPCardsManager : MonoBehaviour
{
    public GameObject[] cardsArr = new GameObject[30]; // �� �ν��Ͻÿ���Ʈ �� ī�� ����
    string[] cardName = new string[]{"Rock", "Scissors", "Paper"}; // ī�� ���� 3�� ������ �ּ� �ڿ� ���� ��
    string root = "Prefab_VoteRSP/"; // ������ �ּ�

    Vector3 VoteEulerAngle =  new Vector3 ( 90, 0, 0); // ó�� ���Ϸ��ޱ�

    public GameObject voteBox; // ���� �� ī�尡 �� voteBox

    public GameObject setCardPosition; // ���� �θ� ������Ʈ
    public Transform[] CardPositionArr; // ���̺� �� ī�� ��ġ ���� �迭

    public GameObject ChooseCardPosition; // ���� �θ� ������Ʈ
    public Transform[] ChoosePositionArr; // ���̺��� ���� ī�� �� �� transform ���� �迭

    public GameObject[] ChooseCards_Opp = new GameObject[3];
    public GameObject ChooseCardPosition_Opp; // ���� �θ� ������Ʈ
    public Transform[] ChoosePositionArr_Opp; // ������ ���̺��� ���� ī�� �� �� transform ���� �迭
    

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
        ChoosePositionArr_Opp = ChooseCardPosition_Opp.GetComponentsInChildren<Transform>();
        //Reset();
    }

    //objectpool�� �ٲپ� �ڽ� sprite�� �ٲٰ� �غ��� 
    public void InstantiateCards()
    {
        //ó���� ī�� ���� 
        for(int i = 0; i < cardsArr.Length; i++)
        {
            // �ν��Ͻÿ���Ʈ ��ġ�� ��ǥ�� ��
            int randomNum = UnityEngine.Random.Range(0, cardName.Length); // 0-2�� �ϳ� �������� ����
            cardsArr[i] = Instantiate(Resources.Load(root + cardName[randomNum]) as GameObject, transform);
            cardsArr[i].transform.eulerAngles = VoteEulerAngle;
        }
    }
    // �Ŵ������� ���� �� ī�� ����, �� �迭�� �����ؾߴ�
    public void VoteCards()
    {
        // ī�� ��ǥ�ڽ� ������ ���� �ڷ�ƾ���� ������� ����
        // �� ī�忡 ������Ʈ �ٿ����� �� �����̴� ������Ʈ
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

    public void SetChooseCardPos(int num, GameObject card)
    {
        card.transform.position = ChoosePositionArr[num].transform.position;
    }

    public void SetChooseCardPos_Opp(int num, GameObject card)
    {
        card.transform.position = ChoosePositionArr_Opp[num+1].transform.position; // �̰� ���� x
        card.transform.eulerAngles = new Vector3(-90, 0, 90);
    }

    public void OppCardsSetRemain() // ���� ī�� ���� ������
    {
        // ī��� �� ���� �ȵ� �ֵ鸸 ã�Ƽ� list�� ���� �� �������� �� �� �����ؼ� �Ŵ����� ����, �Ŵ������� ����ؾ���
        List<RSPCard> cards = new List<RSPCard>(GetComponentsInChildren<RSPCard>()); // ī�� ������Ʈ ���� �ڽĸ� ������

        List<RSPCard> RemainCards = cards.FindAll(t => t.isChoosed == false); //

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

        int randomChice = UnityEngine.Random.Range(0, ChooseCards_Opp.Length);
        // �� ī�� �迭 �Ŵ����� ����
    }

    public void Reset() 
    {
        StartCoroutine(E_Reset());//InstantiateCards();

        for(int i=0; i< ChooseCards_Opp.Length; i++)
        {
            ChooseCards_Opp[i] = null;
        }
    }

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
