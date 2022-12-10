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
        if(FindObjectOfType<UI_Main>() == null) // �� �������� �� ���ٸ� �ı�x
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


    public void MoneyUIUpdate(long moneyupdate) // ���� ������ ���θŴ��� �ӴϾ�����Ʈ ���� ���� �� �̰� �ִϸ��̼�ó�� ����
    {
        string str = string.Format("0:#,###", moneyupdate);
        MoneyUpdateText.GetComponent<Text>().text = str;
        MoneyUpdateText.SetActive(true);
        
        void MoneyUp() 
        {
            MoneyText.GetComponent<Text>().text = string.Format("0:#,###", Manager_Main.Instance.GetMoney());
            MoneyText.SetActive(false); 
        }

        Invoke("MoneyUp", 2); // 2�� �� �� ������Ʈ �ؽ�Ʈ off, ������UI ������Ʈ����
    }

    //TODO : ���⼭ ��� ���ӿ� ���� ������ ���� �ý��� ����
    // �ǵ��� ���θŴ����� ����

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
