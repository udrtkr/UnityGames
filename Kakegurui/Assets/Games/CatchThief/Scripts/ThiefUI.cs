using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefUI : MonoBehaviour
{
    private static ThiefUI _instance;
    public static ThiefUI Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ThiefUI>();
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<ThiefUI>();
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

        if (StartButton == null)
            StartButton = transform.Find("Button_Start").gameObject;
        if (OutButton == null)
            OutButton = transform.Find("Button_Out").gameObject;
        if (JokerPanel == null)
            JokerPanel = transform.Find("JokerPanel").gameObject;
    }
    [SerializeField]
    GameObject StartButton;
    [SerializeField]
    GameObject OutButton;
    [SerializeField]
    GameObject JokerPanel;

    public void Reset()
    {
        StartButton.SetActive(true);
        OutButton.SetActive(true);
        JokerPanel.SetActive(false);
    }

    public void ClickStartButton()
    {
        StartButton.SetActive(false);
        OutButton.SetActive(false);
    }

    void ChoiceJokerPanel() // ��ŸƮ ��ư ������ ���� ��ư ���� �� �� ��Ŀ���� �ƴ��� �����ϴ� �г� ������ �޼���
    {
        JokerPanel.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
