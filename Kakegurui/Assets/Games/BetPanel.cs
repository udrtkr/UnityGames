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
        BetButton.GetComponent<Button>().enabled = true; // ���� ��ư ok

        long _betMoney = BetMoney + 100;
        if (_betMoney <= Manager_Main.Instance.GetMoney()) // ������ �ִ� ������ ������ ok
        {
            BetMoney = _betMoney;
        }
        // ������ �ִ� ������ ũ�� �����Ǹ� �ƹ��ϵ� x

        BetMoneyUpdate();
    }

    public void ClickMinusButton()
    {
        long _betMoney = BetMoney - 100;
        if(_betMoney > 0) // 0���� ũ�� ������ ����
        {
            BetMoney = _betMoney;
        }
        // 0���� ������ �ƹ� �ϵ� x
        BetMoneyUpdate();
    }

    public void ClickBetPanel() // ���� ��ư Ŭ�� ��
    {
        Manager_Main.SetForceStop(false); // ���� ���� ����
        this.gameObject.SetActive(false);
        Reset();
        // ���θŴ������� ������
    }

    public void Reset()
    {
        BetMoney = 100;
        BetMoneyText.GetComponent<Text>().text = UI_Main.NumToStr(BetMoney);
    }
}
