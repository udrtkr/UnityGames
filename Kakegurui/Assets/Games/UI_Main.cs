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
        if(FindObjectOfType<UI_Main>() == null) // 씬 변경했을 때 없다면 파괴x
            DontDestroyOnLoad(this.gameObject);
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject);
        }

        if (MoneyText == null)
            MoneyText = transform.Find("MoneyText").gameObject;
        if (MoneyUpdateText == null)
            MoneyUpdateText = transform.Find("MoneyUpdateText").gameObject;

        MoneyText.SetActive(true);

        MoneyUpdateText.SetActive(false);

    }

    [SerializeField]
    GameObject MoneyText;
    [SerializeField]
    GameObject MoneyUpdateText;


    public void MoneyUIUpdate(long moneyupdate) // 게임 내에서 메인매니저 머니업데이트 먼저 실행 후 이거 애니메이션처럼 실행
    {
        string str = string.Format("0:#,###", moneyupdate);
        MoneyUpdateText.GetComponent<Text>().text = str;
        MoneyUpdateText.SetActive(true);
        
        void MoneyUp() 
        {
            MoneyText.GetComponent<Text>().text = string.Format("0:#,###", Manager_Main.Instance.GetMoney());
            MoneyText.SetActive(false); 
        }

        Invoke("MoneyUp", 2); // 2초 뒤 돈 업데이트 텍스트 off, 돈관련UI 업데이트해줌
    }

    //TODO : 여기서 모든 게임에 적용 가능한 베팅 시스템 만듬
    // 판돈은 메인매니저에 저장

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
