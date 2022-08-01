using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
/// <summary>
/// ���߿�
/// </summary>
public class TalkManager : MonoBehaviour, IPointerClickHandler
{
    private static TalkManager _instance;
    public static TalkManager instance
    {
        get 
        {
            if (_instance == null)
                _instance = new TalkManager();
            return _instance;
        }
    }

    public GameObject talkPanel;
    public Text story;
    private int idx;
    private string thema; // 

    List<Dictionary<string, object>> talkData = new List<Dictionary<string, object>>(); // ��Ȳ�� �°� csv ������ ������, ������ data[listindex]["dictionaryKey=csv�� �� 0��°"]

    private void Awake()
    {
    }
    
    public void GetTalkData(FriendInfo friendInfo) // ��ư ������ �� �����ͺ���
    {
        talkData = CSVReader.Read("TalkData/" + friendInfo.talkDataName);
        thema = friendInfo.idTalk.ToString();
        Debug.Log(talkData[idx][thema]);
        SetStoryText();
    }
    public string GetTalk(int talkIndex)
    {
        return talkData[talkIndex][thema].ToString(); 
    }

    public void SetStoryText()
    {
        story.text = talkData[idx][thema].ToString();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        idx++;
        if (idx >= talkData.Count)
        {
            PlayerUI.instance.SetTalk(false);
            idx = 0;
        }
        SetStoryText();
    }
}