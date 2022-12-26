using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetPanel : MonoBehaviour
{
    [SerializeField]
    GameObject BetMoneyText;
    [SerializeField]
    GameObject PlusButton;
    [SerializeField]
    GameObject MinusButton;
    [SerializeField]
    GameObject BetButton;

    [SerializeField]
    long BetMoney = 100;

    private void Awake()
    {
        if (BetMoneyText == null)
            BetMoneyText = transform.Find("Text_BetMoney").gameObject;
        if (PlusButton == null)
            PlusButton = transform.Find("Button+").gameObject;
        if (MinusButton == null)
            MinusButton = transform.Find("Button-").gameObject;
        if (BetButton == null)
            BetButton = transform.Find("Button_Bet").gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void BetMoneyUpdate()
    {
        BetMoneyText.GetComponent<Text>().text = UI_Main.NumToStr(BetMoney);
    }

    public void ClickPlusButton()
    {
        BetButton.GetComponent<Button>().enabled = true; // 베팅 버튼 ok

        long _betMoney = BetMoney + 100;
        if (_betMoney <= Manager_Main.Instance.GetMoney()) // 가지고 있는 돈보다 작으면 ok
        {
            BetMoney = _betMoney;
        }
        // 가지고 있는 돈보다 크게 측정되면 아무일도 x

        BetMoneyUpdate();
    }

    public void ClickMinusButton()
    {
        long _betMoney = BetMoney - 100;
        if(_betMoney > 0) // 0보다 크면 정상적 동작
        {
            BetMoney = _betMoney;
        }
        // 0보다 작으면 아무 일도 x
        BetMoneyUpdate();
    }

    public void ClickBetPanel() // 베팅 버튼 클릭 시
    {
        Manager_Main.SetForceStop(false); // 강제 멈춤 종료
        this.gameObject.SetActive(false);
        Reset();
        // 메인매니저에서 가조옴
    }

    public void Reset()
    {
        BetMoney = 100;
        BetMoneyText.GetComponent<Text>().text = UI_Main.NumToStr(BetMoney);
    }
}
