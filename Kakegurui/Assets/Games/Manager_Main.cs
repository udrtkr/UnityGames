using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 메인 매니저
/// 전체적인 것들 관리
/// 돈, 씬 변경 
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
        if (FindObjectOfType<Manager_Main>() == null) // 씬 변경했을 때 없다면 파괴x, 데이터 담고있으므로 유지
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
    public void MoneyUpdate(long moneyupdate) // 각 게임에서 돈 업데이트 메소드 불러와서 사용, 매개변수는 돈 변화
    {
        Money += moneyupdate;
        if(Money > 99999999999)
        {
            Money = 99999999999;
        }
    }
}
