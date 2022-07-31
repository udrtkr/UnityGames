using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    [SerializeField] private Text Story;
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
        
    }
    public string GetTalk(int talkIndex)
    {
        return talkData[talkIndex][thema].ToString(); 
    }

    public void SetStoryText(FriendInfo friendInfo)
    {
        Story.text = talkData[idx][friendInfo.idTalk.ToString()].ToString();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        idx++;
        // ��ã���� ������ ���� �� �ٸ����� �������� ����� �� �����̿���
    }
}