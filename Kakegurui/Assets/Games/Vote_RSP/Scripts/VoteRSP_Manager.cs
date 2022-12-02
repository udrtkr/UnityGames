using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���� �� UI���� �ſ� ����Ͽ� Ȯ�� up �� �� ���� �ϴ� �� ����
/// ī�� ���̺����� ����� �� ����Ʈ�ѹ� ++ �Ͽ� 2 �ʰ��� selectOK false, ���� ī��� ī�� ������Ʈ���� select false �� �ٲٱ�
/// ���� ���� ī��� �Ŵ��� ���⿡ ���� ����
/// ī�� ���� ������ �� �÷��̾� �������� �����ְ� ī�� ������Ʈ����? ChooseCardOk true �ؼ� ī�� ���� �� ������, ���� ī�� ���� up
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

    public bool SetOK; // ī�� ���̺��� ���� �غ� ok
    public bool SelectOK; // ���̺����� ī�� 3�� ���� �� ����

    //private Vector3[] VoteCamTransform = new Vector3[] { new Vector3(0, 1.2f, -0.76f), Vector3.zero }; // ������� position eulerangle
    private Dictionary<string, Vector3> VoteCamTransform = new Dictionary<string, Vector3>() { { "position", new Vector3(0, 1.2f, -0.76f) }, { "eulerAngles", Vector3.zero } };
    private Dictionary<string, Vector3> PlayCamTransform = new Dictionary<string, Vector3>() { { "position", new Vector3(0.749000013f, 1.35300004f, 0) }, { "eulerAngles", new Vector3(10.158843f, 270, 0f) } };
    private Dictionary<string, Vector3> TableCamTransform = new Dictionary<string, Vector3>() { {"position", new Vector3(0.519f, 1.75f, 0f) }, {"eulerAngles", new Vector3(66, 270, 0) } };
    //private Vector3[] PlayCamTransform = new Vector3[] { new Vector3(0.749000013f, 1.35300004f, 0), new Vector3(10.158843f, 270, 0f) }; // �÷��̾� ���� Cam
    // Start is called before the first frame update
    


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

    private void Reset()
    {
        Cam.transform.position = VoteCamTransform["position"];
        Cam.transform.eulerAngles = VoteCamTransform["eulerAngles"];
        
        CardsManager.GetComponent<CardsManager>().Reset();
    }

    public void StartVote()
    {
        CardsManager.GetComponent<CardsManager>().VoteCards();
    }

    private void SetCard()
    {
        Cam.transform.position = TableCamTransform["position"];
        Cam.transform.eulerAngles = TableCamTransform["eulerAngles"];

        CardsManager.GetComponent<CardsManager>().SetCards();
    }



    // Update is called once per frame
    void Update()
    {
        if (SetOK)
        {
            SetOK = false;
            SetCard();
        }
    }
}