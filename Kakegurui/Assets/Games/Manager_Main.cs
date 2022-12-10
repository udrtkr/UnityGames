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

    static long Money = 1000;
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
}
