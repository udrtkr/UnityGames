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
            mirrorImage = mirror.transform.Find("imageRSP").gameObject.GetComponent<Image>(); // mirror의 자식 오브젝트에 있는 확률 낮은 카드의 손 이미지 넣을 이미지 컴포넌트
        if (showResult == null)
            showResult = GameObject.FindWithTag("ShowResult");
    }

    public GameObject resetButton; // 게임 다시 시작하는 버튼
    public GameObject outButton; // 밖으로 나가는 버튼 -> 나중에 홀만들고 기능 수정
    public GameObject startButton; // 게임 시작 버튼
    public GameObject mirrorButton; // 거울 on/off 버튼
    public GameObject mirror; // 거울 이미지 담긴 object
    public Image mirrorImage; // mirror의 자식 오브젝트에 있는 이미지, 가위 바위 보 중 확률 제일 낮은 이미지 담을거임

    public GameObject showResult; // 결과보여주는 이미지, 자식에 text있음

    string root = "Prefab_VoteRSP/image"; // 프리팹 주소 + 이미지, 뒤에 Rock Scissor Papper 붙여 이미지 스프라이트 불러옴


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset() // 전체 매니저에서 쓰일 리셋 메서드
    {
        SetResetButton(false);
        SetOutButton(false);
        startButton.SetActive(true);
        SetMirrorButton(false);
        mirror.SetActive(false);
        mirrorButton.GetComponentInChildren<Text>().text = "거울 보기";
        showResult.SetActive(false); // 결과창 닫기
        mirrorImage.GetComponent<imageRSP>().Reset();

    }

    public void Button_Start() // 스타트 버튼 누를 시 실행하는 메서드
    {
        VoteRSP_Manager.Instance.StartVote();
        startButton.SetActive(false);
    }

    public void SetMirrorButton(bool isActive) // 거울 버튼 액티브 제어 메서드
    {
        mirrorButton.SetActive(isActive);
    }

    public void SetMirrorImage(int spriteNum) // 카드 매니저에서 랜덤 낮은 확률 카드 넘버 정해지면 호출
    {
        mirrorImage.sprite = Resources.Load<Sprite>(root + Enum.GetName(typeof(RSPtype), spriteNum)); 
    }
    public void SetMirror() // 거울 버튼 클릭 이벤에 쓰일 메서드
    {
        bool isActive = mirror.activeSelf; // 현재 거울 이미지 상태
        mirror.SetActive(!isActive); // 클릭 시 현재 상태와 반대로 active
        mirrorButton.GetComponentInChildren<Text>().text = !isActive ? "거울 닫기" : "거울 보기"; // 거울 active되는 상태에 따라 버튼 text달라지게
    }

    public void ShowResult(int winWho) // 매니저에서 누가 이겼는지 정수형으로 받아옴
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
        showResult.SetActive(true); // 결과창 액티브 ㅇ
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
