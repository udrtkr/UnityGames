using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 시작 전 UI에서 거울 사용하여 확률 up 된 것 보게 하는 것 까지
/// 카드 테이블에서 골랐을 때 셀렉트넘버 ++ 하여 2 초과면 selectOK false, 고른 카드는 카드 컴포넌트에서 select false 로 바꾸기
/// 세개 고른 카드는 매니저 여기에 정보 전달
/// 카드 세개 가져간 것 플레이어 시점에서 보여주고 카드 컴포넌트에서? ChooseCardOk true 해서 카드 세개 중 고르게, 고른 카드 위로 up
/// 카드 속성 rock scissor papper은 따로 컴포넌트 나누어 자식 컴포넌트로 만들어 붙이기
/// </summary>
public class VoteRSP_Manager : MonoBehaviour
{
    private static VoteRSP_Manager _instance;
    public static VoteRSP_Manager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<VoteRSP_Manager>();
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<VoteRSP_Manager>();
                }
            }

            return _instance;
        }

    }

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

    public GameObject CardsManager;
    public GameObject Cam;

    public bool SetOK; // 카드 테이블에 세팅 준비 ok
    public bool SelectOK; // 테이블에서 카드 3개 고를 수 있음 ok
    public int SelectCardNum = 0; // 플레이어가 테이블에서 고른 카드 수

    public bool turnOutOK = false; // 플레이어가 turnout할 카드 고를 때
    public bool isTurnOut = false; // 위의 상황에서 고른 후 true로 변환, 카드를 낼거임
    public bool ShowResultOK = false; // 모든 상황 끝나고 UI로 결과 보여줄 수 있는지 확인하는 변수

    private int winWho = 0;

    //private Vector3[] VoteCamTransform = new Vector3[] { new Vector3(0, 1.2f, -0.76f), Vector3.zero }; // 순서대로 position eulerangle
    private Dictionary<string, Vector3> VoteCamTransform = new Dictionary<string, Vector3>() { { "position", new Vector3(0, 1.2f, -0.76f) }, { "eulerAngles", Vector3.zero } };
    private Dictionary<string, Vector3> TableCamTransform = new Dictionary<string, Vector3>() { {"position", new Vector3(0.519f, 1.75f, 0f) }, {"eulerAngles", new Vector3(66, 270, 0) } };
    private Dictionary<string, Vector3> PlayCamTransform = new Dictionary<string, Vector3>() { { "position", new Vector3(0.749000013f, 1.35300004f, 0) }, { "eulerAngles", new Vector3(10.158843f, 270, 0f) } }; // 플레이어 시점 Cam
    private Dictionary<string, Vector3> ShowResultCamTransform = new Dictionary<string, Vector3>() { { "position", new Vector3(0, 2.1f, 0) }, {"eulerAngles", new Vector3(90, 0, 0) } };
    
    //private Vector3[] PlayCamTransform = new Vector3[] { new Vector3(0.749000013f, 1.35300004f, 0), new Vector3(10.158843f, 270, 0f) }; // 플레이어 시점 Cam
    // Start is called before the first frame update

    public void Reset()
    {
        Cam.transform.position = VoteCamTransform["position"];
        Cam.transform.eulerAngles = VoteCamTransform["eulerAngles"];

        SetOK = false; 
        SelectOK = false;
        SelectCardNum = 0; 
        turnOutOK = false; 
        isTurnOut = false;
        ShowResultOK = false;

        winWho = 0;

        UIVoteRSP.Instance.Reset();
        CardsManager.GetComponent<RSPCardsManager>().Reset();
    }

    void Start()
    {
        if(Cam == null)
        {
            Cam = GameObject.FindWithTag("MainCamera");
        }
        if (CardsManager == null)
        {
            Cam = GameObject.Find("Cards");
        }

        Reset();
    }

    

    public void StartVote() // 버튼 클릭 시 투표 시작
    {
        CardsManager.GetComponent<RSPCardsManager>().VoteCards();
    }

    private void SetCardTable() // 테이블에 카드 세팅 시 실행하는 메서드
    {
        Cam.transform.position = TableCamTransform["position"];
        Cam.transform.eulerAngles = TableCamTransform["eulerAngles"];

        CardsManager.GetComponent<RSPCardsManager>().SetCards();

        UIVoteRSP.Instance.SetMirrorButton(true);
    }

    private void SetChooseCard1Play() // 테이블에서 세장 카드 모두 고른 후 1개 카드 고르는 상황 시 실행하는 메서드
    {
        // 상대방이 카드 가져가게
        CardsManager.GetComponent<RSPCardsManager>().OppCardsSetRemain(); // 남은 카드들 중 상대방에 3장 부여, 그 중 결과인 하나 랜덤 선택하는 메서드 실행

        // 플레이어 시점 cam으로 변경
        // 세 장 중 낼 카드 결정한 후 냄
        Cam.transform.position = PlayCamTransform["position"];
        Cam.transform.eulerAngles = PlayCamTransform["eulerAngles"];

        turnOutOK = true; // 플레이어가 세 카드 중 한 카드 고를 준비 ok, 카드 고르면 false로, isTunsOut = true로 하고 메서드 실행 
    }

    private void TurnOutCards() // 마지막에 카드를 공개하는 메서드
    {
        Cam.transform.position = ShowResultCamTransform["position"];
        Cam.transform.eulerAngles = ShowResultCamTransform["eulerAngles"];
        CardsManager.GetComponent<RSPCardsManager>().TurnOutCard(); // 카드 천천히 공개하는 메서드
        winWho = CardsManager.GetComponent<RSPCardsManager>().GetCompareCardsPlayerAndOpp(); // 카드 결과 비교해서 결과값 리턴하는 메서드
        Debug.Log(winWho);

        UIVoteRSP.Instance.SetMirrorButton(false);
        UIVoteRSP.Instance.mirror.SetActive(false);
    }
    private void ShowResult()
    {
        UIVoteRSP.Instance.ShowResult(winWho);
        // 플레이어 오브젝트의 애니메이터에서 trigger set하여 애니메이션 이기는 or 지는 것으로 바꿈
        Character_Manager.Instance.SetAnimation(winWho);

    }
    private void AfterShowResult()
    {
        UIVoteRSP.Instance.SetResetButton(true);
        UIVoteRSP.Instance.SetOutButton(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (SetOK)
        {
            SetOK = false;
            SetCardTable();
        }

        if(SelectCardNum >= 3) // 카드 세장 고르면 상태 변경, 세 장 중 한 장 고를 수 있는 상황 만들기
        {
            SelectCardNum = -1;
            SelectOK = false;
            SetChooseCard1Play();
        }

        if (isTurnOut)
        {
            isTurnOut = false;
            TurnOutCards();
        }

        if (ShowResultOK) // 결과 공개하는 UI 세팅
        {
            ShowResultOK = false;
            ShowResult();
            Invoke("AfterShowResult", 3f);
        }
    }
}
