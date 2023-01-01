using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    static long Money = 10000; // 사용자 현재 가지고 있는 돈 액수
    public static long BetMoney = 100; // 베팅 클릭 시 액수 변경하도록

    public static bool BetOK = false; // 베팅 타이밍 위한 변수
    public static bool BetOff = false; // 베팅 후 사용할 변수, 각 게임에서 사용하여 다음 진행

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
    public static void SetMoney(long moneyupdate) // 각 게임에서 돈 업데이트 메소드 불러와서 사용, 매개변수는 돈 변화, UI에서 사용, 이기면 두배 BetMoney*2획득, 지면 x
    {
        Money += moneyupdate;
        if(Money > 99999999999)
        {
            Money = 99999999999;
        }
    }
    public static void Bet() // 베팅 관련 세팅, 각 게임에서 타이밍에 맞게 사용
    {
        UI_Main.Instance.SetBetPanel(true); // 베팅 패널 on
    }

    public static void SetBetMoney(long money)
    {
        BetMoney = money;
    }

    public static void Win() // 이길 시 사용하는 메서드
    {
        SetMoney(2 * BetMoney);
        BetMoney = 100; // 초기화
        UI_Main.Instance.MoneyUIUpdate(2 * BetMoney);
    }
    public static void Lose()
    { 
        //아무 일도 일어나지 않음
        BetMoney = 100; // 초기화
    }

    public void SceneChange(string scenename) // 씬 전환 메서드
    {
        SceneManager.LoadScene(scenename);
    }

    // TODO : 만든 변수 두개 베팅ok 와 강제 스탑 이용하여 게임에 적용
    // 베팅 오케이는 특정한 타이밍에 설정, 
}
