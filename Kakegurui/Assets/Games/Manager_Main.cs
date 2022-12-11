using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    static long Money = 10000;

    public static bool BetOK = false; // ���� Ÿ�̹� ���� ����
    public static bool ForceStop = false; // �����ϴ� Ÿ�ֿ̹� �ٸ� ���ӵ� ������ ��ž�ϴ� ����

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
    public void MoneyUpdate(long moneyupdate) // �� ���ӿ��� �� ������Ʈ �޼ҵ� �ҷ��ͼ� ���, �Ű������� �� ��ȭ
    {
        Money += moneyupdate;
        if(Money > 99999999999)
        {
            Money = 99999999999;
        }
    }
    public static bool GetBetOK() // ���ӿ��� ���� Ÿ�̹� ��� ���� ����� ��
    {
        return BetOK;
    }
    public static void SetBetOK(bool ok)
    {
        BetOK = ok;
    }
    public static void SetForceSop(bool ok)
    {
        ForceStop = ok;
    }
    public static bool GetForceStop()
    {
        return ForceStop;
    }
    public static void Bet() // ���� ���� ����, BetOK�� �� ���� �޼���
    {
        BetOK = false;
        SetForceSop(true);
        UI_Main.Instance.SetBetPanel(true); // ���� �г� on
    }

    // TODO : ���� ���� �ΰ� ����ok �� ���� ��ž �̿��Ͽ� ���ӿ� ����
    // ���� �����̴� Ư���� Ÿ�ֿ̹� ����, 
}
