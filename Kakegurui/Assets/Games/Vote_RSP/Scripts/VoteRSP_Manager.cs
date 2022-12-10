using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���� �� UI���� �ſ� ����Ͽ� Ȯ�� up �� �� ���� �ϴ� �� ����
/// ī�� ���̺��� ����� �� ����Ʈ�ѹ� ++ �Ͽ� 2 �ʰ��� selectOK false, �� ī��� ī�� ������Ʈ���� select false �� �ٲٱ�
/// ���� �� ī��� �Ŵ��� ���⿡ ���� ����
/// ī�� ���� ������ �� �÷��̾� �������� �����ְ� ī�� ������Ʈ����? ChooseCardOk true �ؼ� ī�� ���� �� ����, �� ī�� ���� up
/// ī�� �Ӽ� rock scissor papper�� ���� ������Ʈ ������ �ڽ� ������Ʈ�� ����� ���̱�
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

    public bool SetOK; // ī�� ���̺� ���� �غ� ok
    public bool SelectOK; // ���̺��� ī�� 3�� �� �� ���� ok
    public int SelectCardNum = 0; // �÷��̾ ���̺��� �� ī�� ��

    public bool turnOutOK = false; // �÷��̾ turnout�� ī�� �� ��
    public bool isTurnOut = false; // ���� ��Ȳ���� �� �� true�� ��ȯ, ī�带 ������
    public bool ShowResultOK = false; // ��� ��Ȳ ������ UI�� ��� ������ �� �ִ��� Ȯ���ϴ� ����

    private int winWho = 0;

    //private Vector3[] VoteCamTransform = new Vector3[] { new Vector3(0, 1.2f, -0.76f), Vector3.zero }; // ������� position eulerangle
    private Dictionary<string, Vector3> VoteCamTransform = new Dictionary<string, Vector3>() { { "position", new Vector3(0, 1.2f, -0.76f) }, { "eulerAngles", Vector3.zero } };
    private Dictionary<string, Vector3> TableCamTransform = new Dictionary<string, Vector3>() { {"position", new Vector3(0.519f, 1.75f, 0f) }, {"eulerAngles", new Vector3(66, 270, 0) } };
    private Dictionary<string, Vector3> PlayCamTransform = new Dictionary<string, Vector3>() { { "position", new Vector3(0.749000013f, 1.35300004f, 0) }, { "eulerAngles", new Vector3(10.158843f, 270, 0f) } }; // �÷��̾� ���� Cam
    private Dictionary<string, Vector3> ShowResultCamTransform = new Dictionary<string, Vector3>() { { "position", new Vector3(0, 2.1f, 0) }, {"eulerAngles", new Vector3(90, 0, 0) } };
    
    //private Vector3[] PlayCamTransform = new Vector3[] { new Vector3(0.749000013f, 1.35300004f, 0), new Vector3(10.158843f, 270, 0f) }; // �÷��̾� ���� Cam
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

    

    public void StartVote() // ��ư Ŭ�� �� ��ǥ ����
    {
        CardsManager.GetComponent<RSPCardsManager>().VoteCards();
    }

    private void SetCardTable() // ���̺� ī�� ���� �� �����ϴ� �޼���
    {
        Cam.transform.position = TableCamTransform["position"];
        Cam.transform.eulerAngles = TableCamTransform["eulerAngles"];

        CardsManager.GetComponent<RSPCardsManager>().SetCards();

        UIVoteRSP.Instance.SetMirrorButton(true);
    }

    private void SetChooseCard1Play() // ���̺��� ���� ī�� ��� �� �� 1�� ī�� ���� ��Ȳ �� �����ϴ� �޼���
    {
        // ������ ī�� ��������
        CardsManager.GetComponent<RSPCardsManager>().OppCardsSetRemain(); // ���� ī��� �� ���濡 3�� �ο�, �� �� ����� �ϳ� ���� �����ϴ� �޼��� ����

        // �÷��̾� ���� cam���� ����
        // �� �� �� �� ī�� ������ �� ��
        Cam.transform.position = PlayCamTransform["position"];
        Cam.transform.eulerAngles = PlayCamTransform["eulerAngles"];

        turnOutOK = true; // �÷��̾ �� ī�� �� �� ī�� �� �غ� ok, ī�� ���� false��, isTunsOut = true�� �ϰ� �޼��� ���� 
    }

    private void TurnOutCards() // �������� ī�带 �����ϴ� �޼���
    {
        Cam.transform.position = ShowResultCamTransform["position"];
        Cam.transform.eulerAngles = ShowResultCamTransform["eulerAngles"];
        CardsManager.GetComponent<RSPCardsManager>().TurnOutCard(); // ī�� õõ�� �����ϴ� �޼���
        winWho = CardsManager.GetComponent<RSPCardsManager>().GetCompareCardsPlayerAndOpp(); // ī�� ��� ���ؼ� ����� �����ϴ� �޼���
        Debug.Log(winWho);

        UIVoteRSP.Instance.SetMirrorButton(false);
        UIVoteRSP.Instance.mirror.SetActive(false);
    }
    private void ShowResult()
    {
        UIVoteRSP.Instance.ShowResult(winWho);
        // �÷��̾� ������Ʈ�� �ִϸ����Ϳ��� trigger set�Ͽ� �ִϸ��̼� �̱�� or ���� ������ �ٲ�
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

        if(SelectCardNum >= 3) // ī�� ���� ���� ���� ����, �� �� �� �� �� �� �� �ִ� ��Ȳ �����
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

        if (ShowResultOK) // ��� �����ϴ� UI ����
        {
            ShowResultOK = false;
            ShowResult();
            Invoke("AfterShowResult", 3f);
        }
    }
}
