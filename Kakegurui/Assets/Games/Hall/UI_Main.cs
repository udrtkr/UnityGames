using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Main : MonoBehaviour
{
    private static UI_Main _instance;
    public static UI_Main Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UI_Main>();
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<UI_Main>();
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
        DontDestroyOnLoad(this.gameObject);

        if (MoneyText == null)
            MoneyText = transform.Find("MoneyText").gameObject;
        if (MoneyUpdateText == null)
            MoneyUpdateText = transform.Find("MoneyUpdateText").gameObject;
        if (BetPanel == null)
            BetPanel = transform.Find("BetPanel").gameObject;

        MoneyText.SetActive(true);

        MoneyUpdateText.SetActive(false);

        BetPanel.SetActive(false);

    }

    private void Start()
    {
        MoneyText.GetComponent<Text>().text = NumToStr(Manager_Main.Instance.GetMoney());
    }

    [SerializeField]
    GameObject MoneyText;
    [SerializeField]
    GameObject MoneyUpdateText;
    [SerializeField]
    GameObject BetPanel;

    void MoneyUp()
    {
        MoneyText.GetComponent<Text>().text = NumToStr(Manager_Main.Instance.GetMoney());
        MoneyUpdateText.SetActive(false);
    }
    public void MoneyUIUpdate(long moneyupdate) // 게임 내에서 메인매니저 머니업데이트 먼저 실행 후 이거 애니메이션처럼 실행
    {
        MoneyUpdateText.GetComponent<Text>().text = NumToStr(moneyupdate);
        MoneyUpdateText.SetActive(true);
        
        Invoke("MoneyUp", 1f); // 2초 뒤 돈 업데이트 텍스트 off, 돈관련UI 업데이트해줌
    }

    public static string NumToStr(long num)
    {
        return num.ToString("#,##0");
    }

    //TODO : 여기서 모든 게임에 적용 가능한 베팅 시스템 만듬
    // 판돈은 메인매니저에 저장
    
    public void SetBetPanel(bool set) // 베팅 패널 세팅 메서드
    {
        BetPanel.SetActive(set);
    }

    public void BetClick(long money) // BetPanel에서 사용
    {
        Manager_Main.SetBetMoney(money);
        Manager_Main.SetMoney(-1*money); // 가지고 있는 돈에서 -
        MoneyUIUpdate(-1 * money);

        Manager_Main.SetBetOff(true); // 베팅 끝 메서드 사용
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
