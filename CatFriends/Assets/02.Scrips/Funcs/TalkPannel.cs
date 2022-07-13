using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkPannel : MonoBehaviour
{
    private Text textTalk;
    List<Dictionary<string, object>> talkStory;
    void Start()
    {
        textTalk = GetComponent<Text>();
    }

    public void SetTalkPannel(string friendname)
    {
        talkStory = CSVReader.Read(friendname);
    }

    private void GetTalks(string thema)
    {
        int i = 0;
        while(i < talkStory.Count)
        {
            if (Input.GetMouseButtonDown(0))
            {
                textTalk.text = talkStory[i][thema].ToString();
                i++;
            }
        }
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
