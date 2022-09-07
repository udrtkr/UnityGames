using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �� 20�� �� ÷�� ������ �� �������� ���� 0-19
/// ���� ���ư��鼭 �Ͼ� ����
/// 20����� ������ ��� Opp �� �� ���� ���� Ȯ�� ��������
/// </summary>
public class FingerScaffoldManager : MonoBehaviour
{
    private static FingerScaffoldManager _instance;
    public static FingerScaffoldManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<FingerScaffoldManager>();
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<FingerScaffoldManager>();
                }
            }

            return _instance;
        }
    }
    public bool playerturn = true;

    public GameObject ScaffoldUp;
    private Vector3 ScarroldUp_pos;

    public GameObject ropesObject;
    public GameObject[] ropes;
    public int ropeNum;
    private int[] remainRopefirst = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
    public List<int> remainRope; // ���� ���� ����


    // �հ��� ��/�� f/t �迭
    private bool[] finout= new bool[0]; // 0:player 1:opp1 2:opp2 �� ���� �̿��ؼ� �� ������ �ȿ�����

    public int ropeNumRan = -1;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        ScarroldUp_pos = ScaffoldUp.transform.position;
        ropes = new GameObject[ropesObject.transform.childCount];
        for (int i = 0; i < ropes.Length; i++)
        {
            ropes[i] = ropesObject.transform.GetChild(i).gameObject;
            ropes[i].GetComponent<Rope>().ropeNum = i;
        }
        ropeNumRan = Random.Range(0, ropes.Length);
        remainRope = new List<int>(remainRopefirst);
    }

    public void ScaffoldUp_Down()
    {
        if (ropeNum == ropeNumRan)
        {
            ScaffoldUp.GetComponent<Scaffold_Up>().ScaffoldUp_Down();
        }
        else
            return;
    }

    IEnumerator E_Precedure() 
    { 
        // �ڷ�ƾ ���� �ȿ� ������� �÷��̾���-opp1�� opp2�� 
        // �� �ϸ��� opp�� ���� �Ȼ��� ����
        

        yield return null;
    }
    
    IEnumerator E_PlayerTurn()
    {
        // yield break; �ڷ�ƾ ����������
        yield return null;
    }

    IEnumerator E_Opp1Turn()
    {
        yield return null;
        // waituntil ��� �ϵ帮�� ��
    }

    IEnumerator E_Opp2Turn()
    {
        yield return null;
    }

    public void outHandPlayer() // �÷��̾ ��ư ������
    {

    }


    public void ResetScaffold()
    {
        ScaffoldUp.transform.position = ScarroldUp_pos;
        for (int i = 0; i < ropes.Length; i++)
        {
            ropes[i].SetActive(true);
        }
        ropeNumRan = Random.Range(0, ropes.Length);
        remainRope = new List<int>(remainRopefirst);
    }
}
