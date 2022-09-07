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
    private int[] remainRopefirst = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
    public List<int> remainRope; // 남은 밧줄 저장


    // 손가락 넣/뺗 f/t 배열
    private bool[] finout= new bool[0]; // 0:player 1:opp1 2:opp2 이 조건 이용해서 턴 오는지 안오는지

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
        // 코루틴 와일 안에 순서대로 플레이어턴-opp1턴 opp2턴 
        // 매 턴마다 opp손 뺄지 안뺄지 정함
        

        yield return null;
    }
    
    IEnumerator E_PlayerTurn()
    {
        // yield break; 코루틴 빠져나오기
        yield return null;
    }

    IEnumerator E_Opp1Turn()
    {
        yield return null;
        // waituntil 사용 일드리턴 뒤
    }

    IEnumerator E_Opp2Turn()
    {
        yield return null;
    }

    public void outHandPlayer() // 플레이어가 버튼 누르면
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
