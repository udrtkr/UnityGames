using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefCardsManager : MonoBehaviour
{
    public GameObject[] ThiefCardsArr; // �ڽ� ������Ʈ ī��� A, Joker, A 
    Vector3[] ResetCardsPos = { new Vector3(-0.2f, 0, 0), Vector3.zero, new Vector3(0.2f, 0, 0) };

    private void Awake()
    {
        if(ThiefCardsArr == null)
        {
            ThiefCardsArr = new GameObject[3];
            for(int i = 0; i < ThiefCardsArr.Length; i++)
            {
                ThiefCardsArr[i] = transform.GetChild(i).gameObject; // ù��° �ڽ��� 0��°
            }
        }
    }
    public void Reset()
    {
        for(int i = 0; i < ThiefCardsArr.Length; i++)
        {
            ThiefCardsArr[i].transform.localPosition = ResetCardsPos[i];
            ThiefCardsArr[i].GetComponent<ThiefCard>().Reset();
        }
    }

    // ���� ����, �÷��̾��� ����Ŀ ���� �� ī�� ��ġ, ī���� ���� �����ϴ� �޼���
    public void SetCardsStart(Transform[] firstJoker, Transform notJoker)
    {

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
