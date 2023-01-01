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
    }

    Transform tr;

    string gameName;
    string sceneName;
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
    }

    public void ClickEnterButton()
    {

    }
}
