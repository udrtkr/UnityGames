using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// ���� �Ŵ���
/// ��ü���� �͵� ����
/// ��, �� ���� 
/// </summary>
public class Manager_Main : MonoBehaviour
{
    private static Manager_Main _instance;
    public static Manager_Main Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Manager_Main>();
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<Manager_Main>();
                }
            }

            return _instance;
        }

    }

    private void Awake()
    {
        if (FindObjectOfType<Manager_Main>() == null) // �� �������� �� ���ٸ� �ı�x, ������ ��������Ƿ� ����
            DontDestroyOnLoad(this.gameObject);
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    static long Money = 10000; // ����� ���� ������ �ִ� �� �׼�
    public static long BetMoney = 100; // ���� Ŭ�� �� �׼� �����ϵ���

    public static bool BetOK = false; // ���� Ÿ�̹� ���� ����
    public static bool BetOff = false; // ���� �� ����� ����, �� ���ӿ��� ����Ͽ� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public long GetMoney()
    {
        return Money;
    }
    public static void SetMoney(long moneyupdate) // �� ���ӿ��� �� ������Ʈ �޼ҵ� �ҷ��ͼ� ���, �Ű������� �� ��ȭ, UI���� ���, �̱�� �ι� BetMoney*2ȹ��, ���� x
    {
        Money += moneyupdate;
        if(Money > 99999999999)
        {
            Money = 99999999999;
        }
    }
    public static void Bet() // ���� ���� ����, �� ���ӿ��� Ÿ�ֿ̹� �°� ���
    {
        UI_Main.Instance.SetBetPanel(true); // ���� �г� on
    }

    public static void SetBetMoney(long money)
    {
        BetMoney = money;
    }

    public static void Win() // �̱� �� ����ϴ� �޼���
    {
        SetMoney(2 * BetMoney);
        BetMoney = 100; // �ʱ�ȭ
        UI_Main.Instance.MoneyUIUpdate(2 * BetMoney);
    }
    public static void Lose()
    { 
        //�ƹ� �ϵ� �Ͼ�� ����
        BetMoney = 100; // �ʱ�ȭ
    }

    public void SceneChange(string scenename) // �� ��ȯ �޼���
    {
        SceneManager.LoadScene(scenename);
    }

    // TODO : ���� ���� �ΰ� ����ok �� ���� ��ž �̿��Ͽ� ���ӿ� ����
    // ���� �����̴� Ư���� Ÿ�ֿ̹� ����, 
}
