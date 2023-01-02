using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Door : MonoBehaviour
{
    private static UI_Door _instance;
    public static UI_Door Instance 
    { 
        get 
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UI_Door>();
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<UI_Door>();
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

        tr = GetComponent<Transform>();

        SetPosition(new Vector3(999, 999, 999));
        SetGameNameAndSceneName("", "");

        if (gamenameText == null)
            gamenameText = transform.Find("Text_Game").gameObject;
    }

    Transform tr;

    string gameName;
    string sceneName;

    [SerializeField]
    GameObject gamenameText;
    //Dictionary<string, string> GameScenePair = new Dictionary<string, string>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPosition(Vector3 doorposition)
    {
        tr.position = doorposition + new Vector3(0, 1.215f, -0.35f);
    }
    public void SetGameNameAndSceneName(string gamename, string scenename)
    {
        gameName = gamename;
        sceneName = scenename;
        gamenameText.GetComponent<Text>().text = gameName;
        // textUI도 이에 맞게 설정 게임이름
    }

    public void ClickEnterButton() // 입장하기 버튼 누를 시 방 들어가게, 메인매니저에서 메소드 가져옴
    {
        Manager_Main.SceneChange(sceneName);
    }
}
