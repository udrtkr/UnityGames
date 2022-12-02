using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIVoteRSP : MonoBehaviour
{
    private static UIVoteRSP _instance;
    public static UIVoteRSP Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIVoteRSP>();
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<UIVoteRSP>();
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

        if (startButton == null)
        {
            startButton = GameObject.FindWithTag("Button_Start");
        }
    }

    public GameObject startButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        startButton.SetActive(true);
    }

    public void Button_Start()
    {
        VoteRSP_Manager.Instance.StartVote();
        startButton.SetActive(false);
    }
}
