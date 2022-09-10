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
    private int[] remainRopeInit = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
    public List<int> remainRope; // ���� ���� ����


    // �հ��� ��/�� f/t �迭
    private bool[] Handout= new bool[3]; // 0:player 1:opp1 2:opp2 �� ���� �̿��ؼ� �� ������ �ȿ�����
    private int HandoutPercent = 3;
    public GameObject[] CharsHands = new GameObject[3]; // 0:player 1:opp1 2:opp2 �� �ø��� �Ҷ� ���

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
        remainRope = new List<int>(remainRopeInit);
    }

    IEnumerator E_PlayerTurn()
    {
        playerturn = true;
        // yield break; �ڷ�ƾ ����������
        yield return new WaitUntil(() => playerturn == false);
        HandoutPercent++;
        yield return null;
    }

    IEnumerator E_OppTurn(int OppNum)
    {
        if (Handout[OppNum])
            yield break;
        int num = ChoiceRope_Opp();
        yield return new WaitForSeconds(1); // �հ����� ���������� ����Ű��
        CutRope(num);
        yield return null;
        HandoutPercent++;
        // �հ��� ��ġ �������
        // waituntil ��� �ϵ帮�� ��

    }
    IEnumerator HandUp_Opp(int idx) // ���� �� Handout true �ƴҶ��� Ȯ���ϰ�
    {
        if (!Handout[idx])
        {
            if (HandOut_Opp())
            {
                Handout[idx] = true;
                // �� ���� ��� while�ȿ� 
                Destroy(CharsHands[idx]);
                yield return null;
                // �� �� list�� �߰�
            }
        }
        else
            yield break;
        yield return null;
    }

    public int ChoiceRope_Opp() // Opp�� ���� ropeNum ����
    {
        int n = Random.Range(0, remainRope.Count);
        return n;
    }
    public void CutRope(int ropeN)
    {
        remainRope.Remove(ropeN);
        ropes[ropeN].SetActive(false);
    }

    public bool HandOut_Opp()
    {
        int randomNum = Random.Range(1, 100);
        if (randomNum <= HandoutPercent)
        {
            return true;
        }
        else
            return false;
        // �� ��� ��� +
    }

    public void Handout_Player() // �÷��̾ ��ư ������
    {
        Handout[0] = true;
        // �հ��� ���°� ���
    }
    public bool GameSet() // �� �÷��̾�, Opps �ϸ��� ���
    {
        if ((Handout[0] && Handout[1] && Handout[3]) || remainRope.Count == 0)
            return true;
        else
            return false;
    }

    IEnumerator E_Precedure() 
    {
        // �ڷ�ƾ ���� �ȿ� ������� �÷��̾���-opp1�� opp2�� 
        // �� �ϸ��� opp�� ���� �Ȼ��� ����, Ȯ�� up
        while (true)
        {
            yield return StartCoroutine(E_PlayerTurn());
            yield return null;
            //StartCoroutine(HandUp_Opp(1));
            //StartCoroutine(HandUp_Opp(2));
            yield return new WaitForSeconds(1);
            yield return StartCoroutine(E_OppTurn(1));
            yield return null;
            StartCoroutine(HandUp_Opp(1));
            StartCoroutine(HandUp_Opp(2));
            yield return new WaitForSeconds(1);
            yield return StartCoroutine(E_OppTurn(2));
            StartCoroutine(HandUp_Opp(1));
            StartCoroutine(HandUp_Opp(2));
            yield return new WaitForSeconds(1);
            // HandoutPercent++;
        }

        yield return null;
        // ����� ���� ������
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

    public void ResetScaffold()
    {
        ScaffoldUp.transform.position = ScarroldUp_pos;
        for (int i = 0; i < ropes.Length; i++)
        {
            ropes[i].SetActive(true);
        }
        ropeNumRan = Random.Range(0, ropes.Length);
        remainRope = new List<int>(remainRopeInit);
    }
}
