using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        if (startButton == null)
        {
            startButton = GameObject.FindWithTag("Button_Start");
        }

        if (mirrorButton == null)
        {
            startButton = GameObject.FindWithTag("Button_Mirror");
        }
    }

    public GameObject startButton;
    public GameObject mirrorButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        startButton.SetActive(true);
        mirrorButton.GetComponent<Button>().enabled = false;
    }

    public void Button_Start() // 스타트 버튼 누를 시 실행하는 메서드
    {
        VoteRSP_Manager.Instance.StartVote();
        startButton.SetActive(false);
    }

    public void MirrorOK()
    {
        mirrorButton.GetComponent<Button>().enabled=true;
    }
}
