using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 줄 20개 중 첨에 끊어질 줄 랜덤으로 정함 0-19
/// 세명 돌아가면서 하씩 끊음
/// 20라운드로 갈수록 상대 Opp 둘 손 빼는 랜덤 확률 높아지게
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
    public List<int> remainRope; // 남은 밧줄 저장


    // 손가락 넣/뺗 f/t 배열
    private bool[] Handout= new bool[3]; // 0:player 1:opp1 2:opp2 이 조건 이용해서 턴 오는지 안오는지
    private int HandoutPercent = 3;
    public GameObject[] CharsHands = new GameObject[3]; // 0:player 1:opp1 2:opp2 손 올리게 할때 사용

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
        // yield break; 코루틴 빠져나오기
        yield return new WaitUntil(() => playerturn == false);
        HandoutPercent++;
        yield return null;
    }

    IEnumerator E_OppTurn(int OppNum)
    {
        if (Handout[OppNum])
            yield break;
        int num = ChoiceRope_Opp();
        yield return new WaitForSeconds(1); // 손가락을 로프쪽으로 가리키게
        CutRope(num);
        yield return null;
        HandoutPercent++;
        // 손가락 위치 원래대로
        // waituntil 사용 일드리턴 뒤

    }
    IEnumerator HandUp_Opp(int idx) // 실행 전 Handout true 아닐때만 확인하고
    {
        if (!Handout[idx])
        {
            if (HandOut_Opp())
            {
                Handout[idx] = true;
                // 손 빼는 모션 while안에 
                Destroy(CharsHands[idx]);
                yield return null;
                // 손 뺀 list에 추가
            }
        }
        else
            yield break;
        yield return null;
    }

    public int ChoiceRope_Opp() // Opp에 의한 ropeNum 결정
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
        // 손 드는 모션 +
    }

    public void Handout_Player() // 플레이어가 버튼 누르면
    {
        Handout[0] = true;
        // 손가락 빼는거 모션
    }
    public bool GameSet() // 매 플레이어, Opps 턴마다 사용
    {
        if ((Handout[0] && Handout[1] && Handout[3]) || remainRope.Count == 0)
            return true;
        else
            return false;
    }

    IEnumerator E_Precedure() 
    {
        // 코루틴 와일 안에 순서대로 플레이어턴-opp1턴 opp2턴 
        // 매 턴마다 opp손 뺄지 안뺄지 정함, 확률 up
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
        // 여기는 순위 나오게
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
