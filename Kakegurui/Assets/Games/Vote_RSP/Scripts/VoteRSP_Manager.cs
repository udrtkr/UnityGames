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
    public int SelectCardNum = 0;
    
    public bool turnOutOK = false;

    //private Vector3[] VoteCamTransform = new Vector3[] { new Vector3(0, 1.2f, -0.76f), Vector3.zero }; // 순서대로 position eulerangle
    private Dictionary<string, Vector3> VoteCamTransform = new Dictionary<string, Vector3>() { { "position", new Vector3(0, 1.2f, -0.76f) }, { "eulerAngles", Vector3.zero } };
    private Dictionary<string, Vector3> TableCamTransform = new Dictionary<string, Vector3>() { {"position", new Vector3(0.519f, 1.75f, 0f) }, {"eulerAngles", new Vector3(66, 270, 0) } };
    private Dictionary<string, Vector3> PlayCamTransform = new Dictionary<string, Vector3>() { { "position", new Vector3(0.749000013f, 1.35300004f, 0) }, { "eulerAngles", new Vector3(10.158843f, 270, 0f) } }; // 플레이어 시점 Cam
    //private Vector3[] PlayCamTransform = new Vector3[] { new Vector3(0.749000013f, 1.35300004f, 0), new Vector3(10.158843f, 270, 0f) }; // 플레이어 시점 Cam
    // Start is called before the first frame update

    private void Reset()
    {
        Cam.transform.position = VoteCamTransform["position"];
        Cam.transform.eulerAngles = VoteCamTransform["eulerAngles"];

        SetOK = false;
        SelectOK = false;
        SelectCardNum = 0;
        turnOutOK = false;


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
            Cam = GameObject.FindWithTag("Cards");
        }

        Reset();
    }

    

    public void StartVote()
    {
        CardsManager.GetComponent<RSPCardsManager>().VoteCards();
    }

    private void SetCard()
    {
        Cam.transform.position = TableCamTransform["position"];
        Cam.transform.eulerAngles = TableCamTransform["eulerAngles"];

        CardsManager.GetComponent<RSPCardsManager>().SetCards();
    }

    public void OppCardsSet()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (SetOK)
        {
            SetOK = false;
            SetCard();
        }

        if(SelectCardNum >= 3)
        {
            SelectCardNum = -1;
            SelectOK = false;
            // 상대방이 카드 가져가게
            CardsManager.GetComponent<RSPCardsManager>().OppCardsSetRemain();

            // 플레이어 시점 cam으로 변경
            // 세 장 중 낼 카드 결정한 후 냄
            Cam.transform.position = PlayCamTransform["position"];
            Cam.transform.eulerAngles = PlayCamTransform["eulerAngles"];

            turnOutOK = true; // 플레이어가 한 카드 낼 준비 ok
        }

       
    }
}
