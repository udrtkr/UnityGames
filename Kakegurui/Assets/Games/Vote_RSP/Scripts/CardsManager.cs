using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ī��� �Ŵ�¡���ִ� ������Ʈ
/// ���⼭ ī�� Ȯ�� �������� �� �ϳ� ����
/// </summary>
public class CardsManager : MonoBehaviour
{
    public GameObject[] cardsArr = new GameObject[30]; // �� �ν��Ͻÿ���Ʈ �� ī�� ����
    string[] cardName = new string[]{"Rock", "Scissors", "Paper"}; // ī�� ���� 3�� ������ �ּ� �ڿ� ���� ��
    string root = "Prefab_VoteRSP/"; // ������ �ּ�

    Vector3 VoteEulerAngle =  new Vector3 ( 90, 0, 0); // ó�� ���Ϸ��ޱ�

    public GameObject voteBox; // ���� �� ī�尡 �� voteBox

    public GameObject setCardPosition; // ���� �θ� ������Ʈ

    public Transform[] CardPositionArr; // ���̺� �� ī�� ��ġ ���� �迭

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
        CardPositionArr = setCardPosition.GetComponentsInChildren<Transform>(); // �̶� 0�� �ڱ� �ڽ��̹Ƿ� 1����
        //Reset();
    }
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
