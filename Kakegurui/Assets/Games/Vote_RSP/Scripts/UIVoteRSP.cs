using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIVoteRSP : MonoBehaviour
{
    private static UIVoteRSP _instance;
    public static UIVoteRSP Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIVoteRSP>();
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<UIVoteRSP>();
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

        if (resetButton == null)
        {
            resetButton = GameObject.FindWithTag("Button_Reset");
        }

        if(outButton == null)
        {
            outButton = GameObject.FindWithTag("Button_Out");
        }

        if (startButton == null)
        {
            startButton = GameObject.FindWithTag("Button_Start");
        }

        if (mirrorButton == null)
        {
            startButton = GameObject.FindWithTag("Button_Mirror");
        }
        if(mirrorImage == null)
            mirrorImage = mirror.transform.Find("imageRSP").gameObject.GetComponent<Image>(); // mirror�� �ڽ� ������Ʈ�� �ִ� Ȯ�� ���� ī���� �� �̹��� ���� �̹��� ������Ʈ
        if (showResult == null)
            showResult = GameObject.FindWithTag("ShowResult");
    }

    public GameObject resetButton; // ���� �ٽ� �����ϴ� ��ư
    public GameObject outButton; // ������ ������ ��ư -> ���߿� Ȧ����� ��� ����
    public GameObject startButton; // ���� ���� ��ư
    public GameObject mirrorButton; // �ſ� on/off ��ư
    public GameObject mirror; // �ſ� �̹��� ��� object
    public Image mirrorImage; // mirror�� �ڽ� ������Ʈ�� �ִ� �̹���, ���� ���� �� �� Ȯ�� ���� ���� �̹��� ��������

    public GameObject showResult; // ��������ִ� �̹���, �ڽĿ� text����

    string root = "Prefab_VoteRSP/image"; // ������ �ּ� + �̹���, �ڿ� Rock Scissor Papper �ٿ� �̹��� ��������Ʈ �ҷ���


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset() // ��ü �Ŵ������� ���� ���� �޼���
    {
        SetResetButton(false);
        SetOutButton(false);
        startButton.SetActive(true);
        SetMirrorButton(false);
        mirror.SetActive(false);
        mirrorButton.GetComponentInChildren<Text>().text = "�ſ� ����";
        showResult.SetActive(false); // ���â �ݱ�
        mirrorImage.GetComponent<imageRSP>().Reset();

    }

    public void Button_Start() // ��ŸƮ ��ư ���� �� �����ϴ� �޼���
    {
        VoteRSP_Manager.Instance.StartVote();
        startButton.SetActive(false);
    }

    public void SetMirrorButton(bool isActive) // �ſ� ��ư ��Ƽ�� ���� �޼���
    {
        mirrorButton.SetActive(isActive);
    }

    public void SetMirrorImage(int spriteNum) // ī�� �Ŵ������� ���� ���� Ȯ�� ī�� �ѹ� �������� ȣ��
    {
        mirrorImage.sprite = Resources.Load<Sprite>(root + Enum.GetName(typeof(RSPtype), spriteNum)); 
    }
    public void SetMirror() // �ſ� ��ư Ŭ�� �̺��� ���� �޼���
    {
        bool isActive = mirror.activeSelf; // ���� �ſ� �̹��� ����
        mirror.SetActive(!isActive); // Ŭ�� �� ���� ���¿� �ݴ�� active
        mirrorButton.GetComponentInChildren<Text>().text = !isActive ? "�ſ� �ݱ�" : "�ſ� ����"; // �ſ� active�Ǵ� ���¿� ���� ��ư text�޶�����
    }

    public void ShowResult(int winWho) // �Ŵ������� ���� �̰���� ���������� �޾ƿ�
    {
        Text resulttext = showResult.GetComponentInChildren<Text>();
        switch (winWho)
        {
            case 0: resulttext.text = "Draw!";
                break;
            case 1: resulttext.text = "You Win!!";
                break ;
            case -1: resulttext.text = "You Lose...";
                break;
            default:
                break;
        }
        showResult.SetActive(true); // ���â ��Ƽ�� ��
    }
    public void SetResetButton(bool isActive)
    {
        resetButton.SetActive(isActive);
    }

    public void SetOutButton(bool isActive)
    {
        outButton.SetActive(isActive);
    }
    public void ClickResetButton()
    {
        SetResetButton(false);
        VoteRSP_Manager.Instance.Reset();
    }
}
