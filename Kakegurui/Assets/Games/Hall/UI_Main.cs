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
    public void MoneyUIUpdate(long moneyupdate) // ���� ������ ���θŴ��� �ӴϾ�����Ʈ ���� ���� �� �̰� �ִϸ��̼�ó�� ����
    {
        MoneyUpdateText.GetComponent<Text>().text = NumToStr(moneyupdate);
        MoneyUpdateText.SetActive(true);
        
        Invoke("MoneyUp", 1f); // 2�� �� �� ������Ʈ �ؽ�Ʈ off, ������UI ������Ʈ����
    }

    public static string NumToStr(long num)
    {
        return num.ToString("#,##0");
    }

    //TODO : ���⼭ ��� ���ӿ� ���� ������ ���� �ý��� ����
    // �ǵ��� ���θŴ����� ����
    
    public void SetBetPanel(bool set) // ���� �г� ���� �޼���
    {
        BetPanel.SetActive(set);
    }

    public void BetClick(long money) // BetPanel���� ���
    {
        Manager_Main.SetBetMoney(money);
        Manager_Main.SetMoney(-1*money); // ������ �ִ� ������ -
        MoneyUIUpdate(-1 * money);

        Manager_Main.SetBetOff(true); // ���� �� �޼��� ���
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
